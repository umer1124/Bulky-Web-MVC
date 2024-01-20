using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ROLE_ADMIN)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderViewModel orderViewModel { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            orderViewModel = new()
            {
                orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderId, includeProperties: "ApplicationUser"),
                orderDetail = _unitOfWork.OrderDetail.GetAll(item => item.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ActionName("Details")]
        public IActionResult DetailsPayNow()
        {
            orderViewModel.orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderViewModel.orderHeader.Id, includeProperties: "ApplicationUser");
            orderViewModel.orderDetail = _unitOfWork.OrderDetail.GetAll(item => item.OrderHeaderId == orderViewModel.orderHeader.Id, includeProperties: "Product");

            // Add Strip payment logic here
            //string domain = "https://localhost:7160";
            string domain = "https://bulkymvc.azurewebsites.net";

            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{domain}/admin/order/PaymentConfirmation?orderHeaderId={orderViewModel.orderHeader.Id}",
                CancelUrl = $"{domain}/admin/order/details?orderId={orderViewModel.orderHeader.Id}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in orderViewModel.orderDetail)
            {
                var sessionLineItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.Price * 100), // $20.50 => 2050 
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

            _unitOfWork.OrderHeader.UpdateStripePaymentId(orderViewModel.orderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();

            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }

        public IActionResult PaymentConfirmation(int orderHeaderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderHeaderId);

            if (orderHeader != null)
            {
                if (orderHeader.PaymentStatus == SD.PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT)
                {
                    // This is an order by Company

                    var service = new SessionService();
                    Session session = service.Get(orderHeader.SessionId);

                    if (session.PaymentStatus.ToLower() == "paid")
                    {
                        _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                        _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, SD.PAYMENT_STATUS_APPROVED);

                        _unitOfWork.Save();
                    }
                }
            }

            return View(orderHeaderId);
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_EMPLOYEE)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(item => item.Id == orderViewModel.orderHeader.Id);

            orderHeaderFromDb.Name = orderViewModel.orderHeader.Name;
            orderHeaderFromDb.StreetAddress = orderViewModel.orderHeader.StreetAddress;
            orderHeaderFromDb.City = orderViewModel.orderHeader.City;
            orderHeaderFromDb.State = orderViewModel.orderHeader.State;
            orderHeaderFromDb.PostalCode = orderViewModel.orderHeader.PostalCode;
            orderHeaderFromDb.PhoneNumber = orderViewModel.orderHeader.PhoneNumber;

            if (!string.IsNullOrEmpty(orderViewModel.orderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = orderViewModel.orderHeader.Carrier;
            }

            if (!string.IsNullOrEmpty(orderViewModel.orderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = orderViewModel.orderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Order Details Updated Successfully";

            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_EMPLOYEE)]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderViewModel.orderHeader.Id, SD.ORDER_STATUS_PROCESSING);
            _unitOfWork.Save();

            TempData["success"] = "Order Status Updated Successfully";

            return RedirectToAction(nameof(Details), new { orderId = orderViewModel.orderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_EMPLOYEE)]
        public IActionResult ShipOrder()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(item => item.Id == orderViewModel.orderHeader.Id);

            orderHeaderFromDb.Carrier = orderViewModel.orderHeader.Carrier;
            orderHeaderFromDb.TrackingNumber = orderViewModel.orderHeader.TrackingNumber;
            orderHeaderFromDb.OrderStatus = SD.ORDER_STATUS_SHIPPED;
            orderHeaderFromDb.ShippingDate = DateTime.Now;

            if (orderHeaderFromDb.PaymentStatus == SD.PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT)
            {
                orderHeaderFromDb.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Order Shipped Successfully";

            return RedirectToAction(nameof(Details), new { orderId = orderViewModel.orderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_EMPLOYEE)]
        public IActionResult CancelOrder()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(item => item.Id == orderViewModel.orderHeader.Id);

            if (orderHeaderFromDb.PaymentStatus == SD.PAYMENT_STATUS_APPROVED)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeaderFromDb.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.Id, SD.ORDER_STATUS_CANCELLED, SD.ORDER_STATUS_REFUNDED);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.Id, SD.ORDER_STATUS_CANCELLED, SD.ORDER_STATUS_CANCELLED);
            }

            _unitOfWork.Save();

            TempData["success"] = "Order Cancelled Successfully";

            return RedirectToAction(nameof(Details), new { orderId = orderViewModel.orderHeader.Id });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> list = [];

            if (User.IsInRole(SD.ROLE_ADMIN) || User.IsInRole(SD.ROLE_EMPLOYEE))
            {
                list = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else 
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                list = _unitOfWork.OrderHeader.GetAll(item => item.ApplicationUserId == userId, includeProperties: "ApplicationUser").ToList();
            }
            
            switch (status)
            {
                case "inprocess":
                    list = list.Where(item => item.OrderStatus == SD.PAYMENT_STATUS_APPROVED_FOR_DELAYED_PAYMENT);
                    break;

                case "pending":
                    list = list.Where(item => item.OrderStatus == SD.ORDER_STATUS_PROCESSING);
                    break;

                case "completed":
                    list = list.Where(item => item.OrderStatus == SD.ORDER_STATUS_SHIPPED);
                    break;

                case "approved":
                    list = list.Where(item => item.OrderStatus == SD.ORDER_STATUS_APPROVED);
                    break;

                default:
                    break;
            }

            return Json(new { data = list });
        }

        #endregion
    }
}
