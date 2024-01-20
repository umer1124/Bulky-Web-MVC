using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {

            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                { 
                    _db.Database.Migrate();
                }
            }
            catch(Exception exception) { }

            // Create Roles
            if (!_roleManager.RoleExistsAsync(SD.ROLE_ADMIN).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_ADMIN)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                { 
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    Name = "Admin",
                    PhoneNumber = "+921231234567",
                    StreetAddress = "Gulburg Greens",
                    City =  "Islamabad",
                    State = "Isb",
                    PostalCode = "46000"
                }, "Abc@123").GetAwaiter().GetResult();

                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Email == "admin@mail.com");
                _userManager.AddToRoleAsync(applicationUser, SD.ROLE_ADMIN).GetAwaiter().GetResult();
            }

            if (!_roleManager.RoleExistsAsync(SD.ROLE_COMPANY).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_COMPANY)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "company@mail.com",
                    Email = "company@mail.com",
                    Name = "Company",
                    PhoneNumber = "+921231234567",
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad",
                    State = "Isb",
                    PostalCode = "46000"
                }, "Abc@123").GetAwaiter().GetResult();

                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Email == "company@mail.com");
                _userManager.AddToRoleAsync(applicationUser, SD.ROLE_COMPANY).GetAwaiter().GetResult();
            }

            if (!_roleManager.RoleExistsAsync(SD.ROLE_CUSTOMER).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_CUSTOMER)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "customer@mail.com",
                    Email = "customer@mail.com",
                    Name = "Customer",
                    PhoneNumber = "+921231234567",
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad",
                    State = "Isb",
                    PostalCode = "46000"
                }, "Abc@123").GetAwaiter().GetResult();

                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Email == "customer@mail.com");
                _userManager.AddToRoleAsync(applicationUser, SD.ROLE_CUSTOMER).GetAwaiter().GetResult();
            }

            if (!_roleManager.RoleExistsAsync(SD.ROLE_EMPLOYEE).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_EMPLOYEE)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "employee@mail.com",
                    Email = "employee@mail.com",
                    Name = "Employee",
                    PhoneNumber = "+921231234567",
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad",
                    State = "Isb",
                    PostalCode = "46000"
                }, "Abc@123").GetAwaiter().GetResult();

                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Email == "employee@mail.com");
                _userManager.AddToRoleAsync(applicationUser, SD.ROLE_EMPLOYEE).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
