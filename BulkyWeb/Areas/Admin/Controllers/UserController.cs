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

        private readonly ApplicationDbContext _db;

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            ApplicationUser? applicationUser = _db.Users.FirstOrDefault(item => item.Id == userId) as ApplicationUser;
            if (applicationUser == null) 
            {
                return NotFound();
            }

            var roles = _db.Roles.ToList();
            var userRole = _db.UserRoles.FirstOrDefault(item => item.UserId == userId);
            if (userRole == null)
            {
                return NotFound();
            }

            var userCompanyId = applicationUser.CompanyId;

            RoleManagementViewModel userRoleViewModel = new()
            {
                UserId = userId,
                Name = applicationUser.Name,
                RoleId = userRole.RoleId,
                RoleList = _db.Roles.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CompanyId = userCompanyId,
                CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(userRoleViewModel);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementViewModel userRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var userRole = _db.UserRoles.FirstOrDefault(item => item.UserId == userRoleViewModel.UserId);
                if (userRole.RoleId != userRoleViewModel.RoleId) 
                {
                    // Update UserRole in db
                    _db.UserRoles.Remove(userRole);
                    _db.SaveChanges();

                    userRole.RoleId = userRoleViewModel.RoleId;
                    _db.UserRoles.Add(new()
                    { 
                        RoleId = userRoleViewModel.RoleId,
                        UserId = userRoleViewModel.UserId,
                    });
                    _db.SaveChanges();
                }

                ApplicationUser applicationUser = _db.Users.FirstOrDefault(item => item.Id == userRoleViewModel.UserId) as ApplicationUser;

                if (applicationUser.CompanyId == userRoleViewModel.CompanyId)
                {
                    TempData["success"] = "User Role updated successfully";

                    return RedirectToAction("Index");
                }

                var isCompanyRoleSelected = _db.Roles.FirstOrDefault(item => item.Id == userRoleViewModel.RoleId && item.Name.ToLower() == "company") == null ? false : true;

                if (isCompanyRoleSelected)
                {
                    applicationUser.CompanyId = userRoleViewModel.CompanyId;
                }
                else
                {
                    applicationUser.CompanyId = null;
                }

                _db.Users.Update(applicationUser);

                _db.SaveChanges();                

                TempData["success"] = "User Role updated successfully";

                return RedirectToAction("Index");
            }

            userRoleViewModel.RoleList = _db.Roles.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            
            userRoleViewModel.CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(userRoleViewModel);
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
