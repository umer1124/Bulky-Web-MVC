using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ROLE_ADMIN)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _db;

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
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

            ApplicationUser? applicationUser = _db.ApplicationUsers.Include(item => item.Company).FirstOrDefault(item => item.Id == userId);
            if (applicationUser == null) 
            {
                return NotFound();
            }

            var userRole = _db.UserRoles.FirstOrDefault(item => item.UserId == userId);
            if (userRole == null)
            {
                return NotFound();
            }

            applicationUser.Role = _db.Roles.FirstOrDefault(role => role.Id == userRole.RoleId).Name;

            RoleManagementViewModel userRoleViewModel = new()
            {
                ApplicationUser = applicationUser,
                RoleList = _db.Roles.ToList().Select(i => new SelectListItem
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

            // Bug when change only company face issue
            var userRole = _db.UserRoles.FirstOrDefault(item => item.UserId == roleManagementViewModel.ApplicationUser.Id);
            var oldRole = _db.Roles.FirstOrDefault(item => item.Id == userRole.RoleId);

            if (!(roleManagementViewModel.ApplicationUser.Role == oldRole.Name))
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Id == roleManagementViewModel.ApplicationUser.Id);
                if (roleManagementViewModel.ApplicationUser.Role == SD.ROLE_COMPANY)
                {
                    applicationUser.CompanyId = roleManagementViewModel.ApplicationUser.CompanyId;
                }

                if (oldRole.Name == SD.ROLE_COMPANY)
                {
                    applicationUser.CompanyId = null;
                }

                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole.Name).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementViewModel.ApplicationUser.Role).GetAwaiter().GetResult();
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
            List<ApplicationUser> list = _db.ApplicationUsers.Include(item => item.Company).ToList();

            var roles = _db.Roles.ToList();
            var userRoles = _db.UserRoles.ToList();

            foreach (var user in list)
            {
                string roleId = userRoles.FirstOrDefault(item => item.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(item => item.Id == roleId).Name;

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
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(item => item.Id == id);
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

            _db.SaveChanges();

            return Json(new { success = true, message = statusMessage });
        }

        #endregion
    }
}
