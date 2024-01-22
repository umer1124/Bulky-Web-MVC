using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ROLE_ADMIN)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            ApplicationUser? applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId, includeProperties: "Company");
            if (applicationUser == null) 
            {
                return NotFound();
            }

            var userRole = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult();
            if (userRole == null)
            {
                return NotFound();
            }

            applicationUser.Role = userRole.FirstOrDefault();

            RoleManagementViewModel userRoleViewModel = new()
            {
                ApplicationUser = applicationUser,
                RoleList = _roleManager.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(userRoleViewModel);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementViewModel roleManagementViewModel)
        {
            ApplicationUser? applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == roleManagementViewModel.ApplicationUser.Id, includeProperties: "Company");
            if (applicationUser == null)
            {
                return NotFound();
            }

            var userRole = _userManager.GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();

            if (!(roleManagementViewModel.ApplicationUser.Role == userRole))
            {
                if (roleManagementViewModel.ApplicationUser.Role == SD.ROLE_COMPANY)
                {
                    applicationUser.CompanyId = roleManagementViewModel.ApplicationUser.CompanyId;
                }

                if (userRole == SD.ROLE_COMPANY)
                {
                    applicationUser.CompanyId = null;
                }

                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(applicationUser, userRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementViewModel.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            else
            {
                if (userRole == SD.ROLE_COMPANY && applicationUser.CompanyId != roleManagementViewModel.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = roleManagementViewModel.ApplicationUser.CompanyId;
                    
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }

            TempData["success"] = "User Role updated successfully";

            return RedirectToAction("Index");

           /* roleManagementViewModel.RoleList = _db.Roles.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            roleManagementViewModel.CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(roleManagementViewModel);*/
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<ApplicationUser> list = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();
            
            foreach (var user in list)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                if (user.Company == null)
                {
                    user.Company = new()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = list });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var userFromDb = _unitOfWork.ApplicationUser.Get(item => item.Id == id);
            if (userFromDb == null) 
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            string statusMessage = "";

            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                // User is currently locked and we need to unlock it
                userFromDb.LockoutEnd = DateTime.UtcNow;
                statusMessage = "User Unlocked successfully";
            }
            else
            {
                userFromDb.LockoutEnd = DateTime.UtcNow.AddYears(3);
                statusMessage = "User Locked for 3 years successfully";
            }

            _unitOfWork.ApplicationUser.Update(userFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = statusMessage });
        }

        #endregion
    }
}
