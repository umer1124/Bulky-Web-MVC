using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null && claim.Value != null)
            {
                // logged in user
                int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == claim.Value);
                HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);
            }

            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");
            return View(products);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                ProductId = productId,
                Count = 1,
                Product = _unitOfWork.Product.Get(item => item.Id == productId, includeProperties: "Category,ProductImages")
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = userId;

            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(item => item.ProductId == shoppingCart.ProductId && item.ApplicationUserId == userId);
            if (shoppingCartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                shoppingCartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
            }

            _unitOfWork.Save();

            int shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == userId);
            HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);

            TempData["success"] = "Cart updated successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
