using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null && claim.Value != null)
            {
                int shoppingCartCount = 0;
                int? sessionShoppingCartCount = HttpContext.Session.GetInt32(SD.SESSION_SHOPPING_CART);

                if (sessionShoppingCartCount == null)
                {
                    shoppingCartCount = _unitOfWork.ShoppingCart.Count(item => item.ApplicationUserId == claim.Value);
                    HttpContext.Session.SetInt32(SD.SESSION_SHOPPING_CART, shoppingCartCount);
                }

                shoppingCartCount = (int)sessionShoppingCartCount;

                return View(shoppingCartCount);
            }

            HttpContext.Session.Clear();
            return View(0);
        }

    }
}
