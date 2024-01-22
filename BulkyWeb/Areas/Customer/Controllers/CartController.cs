using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ShoppingCartViewModel shoppingCartViewModel { get; set; }

        public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == userId);
            HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);

            shoppingCartViewModel = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product").ToList(),
                OrderHeader = new()
            };

            var productIds = shoppingCartViewModel.ShoppingCartList.Select(item => item.ProductId).ToList();
            IEnumerable<ProductImage> productImages = _unitOfWork.ProductImage.GetAll(item => productIds.Contains(item.ProductId)).ToList();

            foreach (var cart in shoppingCartViewModel.ShoppingCartList)
            { 
                cart.Product.ProductImages = productImages.Where(item => item.ProductId == cart.ProductId).ToList();
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(shoppingCartViewModel);
        }

        public IActionResult Plus(int cartId) 
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(item => item.Id == cartId);
            cartFromDb.Count += 1;

            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(item => item.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else 
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            
            _unitOfWork.Save();

            int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == cartFromDb.ApplicationUserId);
            HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(item => item.Id == cartId);

            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();

            int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == cartFromDb.ApplicationUserId);
            HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartViewModel = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product").ToList(),
                OrderHeader = new()
            };

            shoppingCartViewModel.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId);

            shoppingCartViewModel.OrderHeader.ApplicationUserId = shoppingCartViewModel.OrderHeader.ApplicationUser.Id;
            shoppingCartViewModel.OrderHeader.Name = shoppingCartViewModel.OrderHeader.ApplicationUser.Name;
            shoppingCartViewModel.OrderHeader.StreetAddress = shoppingCartViewModel.OrderHeader.ApplicationUser.StreetAddress;
            shoppingCartViewModel.OrderHeader.City = shoppingCartViewModel.OrderHeader.ApplicationUser.City;
            shoppingCartViewModel.OrderHeader.State = shoppingCartViewModel.OrderHeader.ApplicationUser.State;
            shoppingCartViewModel.OrderHeader.PostalCode = shoppingCartViewModel.OrderHeader.ApplicationUser.PostalCode;
            shoppingCartViewModel.OrderHeader.PhoneNumber = shoppingCartViewModel.OrderHeader.ApplicationUser.PhoneNumber;

            foreach (var cart in shoppingCartViewModel.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(shoppingCartViewModel);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartViewModel.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product").ToList();


            shoppingCartViewModel.OrderHeader.ApplicationUserId = userId;
            shoppingCartViewModel.OrderHeader.OrderDate = DateTime.Now;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId);

            foreach (var cart in shoppingCartViewModel.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // It is a regular customer account and we need to capture payment
                shoppingCartViewModel.OrderHeader.OrderStatus = SD.ORDER_STATUS_PENDING;
                shoppingCartViewModel.OrderHeader.PaymentStatus = SD.PAYMENT_STATUS_PENDING;
            }
            else
            {
                // It is a company user
                shoppingCartViewModel.OrderHeader.OrderStatus = SD.ORDER_STATUS_APPROVED;
                shoppingCartViewModel.OrderHeader.PaymentStatus = SD.PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT;
            }

            _unitOfWork.OrderHeader.Add(shoppingCartViewModel.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in shoppingCartViewModel.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = shoppingCartViewModel.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count,
                };

                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // It is a regular customer account and we need to capture payment
                // Add Strip payment logic here

                string domain = Request.Scheme + "://" + Request.Host.Value;

                var options = new SessionCreateOptions
                {
                    SuccessUrl = $"{domain}/customer/cart/OrderConfirmation?id={shoppingCartViewModel.OrderHeader.Id}",
                    CancelUrl = $"{domain}/customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in shoppingCartViewModel.ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions() 
                        { 
                            UnitAmount = (long) (item.Price * 100), // $20.50 => 2050 
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions() 
                            {
                                Name = item.Product.Title
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStripePaymentId(shoppingCartViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();

                Response.Headers.Add("Location", session.Url);

                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCartViewModel.OrderHeader.Id });
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == id, includeProperties: "ApplicationUser");

            if (orderHeader != null)
            {
                if (orderHeader.PaymentStatus != SD.PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT)
                { 
                    // This is an order by Customer

                    var service = new SessionService();
                    Session session = service.Get(orderHeader.SessionId);

                    if (session.PaymentStatus.ToLower() == "paid")
                    {
                        _unitOfWork.OrderHeader.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                        _unitOfWork.OrderHeader.UpdateStatus(id, SD.ORDER_STATUS_APPROVED, SD.PAYMENT_STATUS_APPROVED);
                        
                        _unitOfWork.Save();
                    }
                }

                List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

                _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
                _unitOfWork.Save();

                int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == orderHeader.ApplicationUserId);
                HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);
            }

            return View(id);
        }

		private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else 
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}