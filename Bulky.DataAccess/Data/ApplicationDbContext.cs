using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // when we have IdentityDbContext we have to add base.OnModelCreating(modelBuilder)
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "137c8e30-8011-4e13-835a-9ed6056b09a6",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = null
                },
                new IdentityRole
                {
                    Id = "2e6eb075-8360-4354-b40b-29f257b319b1",
                    Name = "Company",
                    NormalizedName = "COMPANY",
                    ConcurrencyStamp = null
                },
                new IdentityRole
                {
                    Id = "3c09f235-e3a2-4530-82e2-257b1f6ead6c",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = null
                },
                new IdentityRole
                {
                    Id = "803ee787-bbdc-44e7-8a22-9976391b3182",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = null
                }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "801999c1-00c3-4a24-9127-84273dd7d267",
                    UserName = "admin@mail.com",
                    NormalizedUserName = "ADMIN@MAIL.COM",
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEFm95zm6s8AEWYAXKqsJz9hpcVL1O/eKknXQsabPr+1XHVDg1QL0q3o3gql/+UflNQ==",
                    SecurityStamp = "3VXUHXJFIN2DCDKS3XYN2PI533DQQ47H",
                    ConcurrencyStamp = "e692065d-79c6-41b3-ac96-be818e5e6422",
                    PhoneNumber = "+921231234567",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    City = "Islamabad",
                    Name = "Administrator",
                    PostalCode = "44000",
                    State = "Punjab",
                    StreetAddress = "Gulburg greens",
                    CompanyId = null
                },
                new ApplicationUser
                {
                    Id = "f17444f0-8643-45ef-9f4e-71f7f90b5a95",
                    UserName = "company@mail.com",
                    NormalizedUserName = "COMPANY@MAIL.COM",
                    Email = "company@mail.com",
                    NormalizedEmail = "COMPANY@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEM7Hz2KjQMFZkx9nAqCZivwRQVatNIWAdMDRBHmOWR1ktJBIQ+AFOQFI+RcsFIMXCQ==",
                    SecurityStamp = "RYDPS4RCBJB6H6YC6O7OIN7JDGXOTMI7",
                    ConcurrencyStamp = "9913189b-ff1e-45cb-b841-b4b3af24d48e",
                    PhoneNumber = "+921231234567",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    City = "Islamabad",
                    Name = "Company",
                    PostalCode = "44000",
                    State = "Punjab",
                    StreetAddress = "Gulburg greens",
                    CompanyId = 1
                },
                new ApplicationUser
                {
                    Id = "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a",
                    UserName = "employee@mail.com",
                    NormalizedUserName = "EMPLOYEE@MAIL.COM",
                    Email = "employee@mail.com",
                    NormalizedEmail = "EMPLOYEE@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAECk8dlZBCbwCFCh2NUtIfvSSnbf2i6OSV2MBcoRm0xQCnFl5HRyYIK7DMWlqicuuPg==",
                    SecurityStamp = "B3VW3NGJPLJIB7VAJPHE4ZQXQRXNG5GT",
                    ConcurrencyStamp = "7287cd7b-087c-49c1-afda-257446473577",
                    PhoneNumber = "+921231234567",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    City = "Islamabad",
                    Name = "Employee",
                    PostalCode = "44000",
                    State = "Punjab",
                    StreetAddress = "Gulburg greens",
                    CompanyId = null
                },
                new ApplicationUser
                {
                    Id = "61244749-ec1c-45fc-b3ef-9287c90491db",
                    UserName = "customer@mail.com",
                    NormalizedUserName = "CUSTOMER@MAIL.COM",
                    Email = "customer@mail.com",
                    NormalizedEmail = "CUSTOMER@MAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEMRda8maytGXXAtbxoKSDB28wfRgCaOgXkpIAur+G2O129pb3Ry9CTLs48ixYXu9aA==",
                    SecurityStamp = "F4JXHJNQUW3Y24IDGBQL57S4OMFFESUI",
                    ConcurrencyStamp = "d909fab9-2a74-421f-80a3-423e7a63d46c",
                    PhoneNumber = "+921231234567",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    City = "Islamabad",
                    Name = "Customer",
                    PostalCode = "44000",
                    State = "Punjab",
                    StreetAddress = "Gulburg greens",
                    CompanyId = null
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                {
                    UserId = "801999c1-00c3-4a24-9127-84273dd7d267",
                    RoleId = "137c8e30-8011-4e13-835a-9ed6056b09a6"
                },
                new IdentityUserRole<string>
                {
                    UserId = "f17444f0-8643-45ef-9f4e-71f7f90b5a95",
                    RoleId = "2e6eb075-8360-4354-b40b-29f257b319b1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a",
                    RoleId = "3c09f235-e3a2-4530-82e2-257b1f6ead6c"
                },
                new IdentityUserRole<string> 
                {
                    UserId = "61244749-ec1c-45fc-b3ef-9287c90491db",
                    RoleId = "803ee787-bbdc-44e7-8a22-9976391b3182"
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id = 1,
                    Name = "Action", 
                    DisplayOrder = 1 
                },
                new Category { 
                    Id = 2, 
                    Name = "SciFi", 
                    DisplayOrder = 2
                },
                new Category { 
                    Id = 3, 
                    Name = "History", 
                    DisplayOrder = 3 
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = @"\images\product\f8bcff4f-3fb2-4b86-99b8-9eeed58e70fe.jpg"
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = @"\images\product\0151bb54-ad86-41e8-acae-9448e2019d31.jpg"
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 1,
                    ImageUrl = @"\images\product\293b3549-a885-41b8-b4d7-7a184d642c70.jpg"
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl = @"\images\product\d9440830-a555-4835-b64e-16a31bd0c755.jpg"
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl = @"\images\product\e1be1146-e49c-4857-a3e2-ba5717114a1c.jpg"
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = @"\images\product\ef2f64ef-4eb3-4c96-a87c-5310ae658a61.jpg"
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { 
                    Id = 1, 
                    Name = "TeraData", 
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad", 
                    PhoneNumber = "+921231234567",
                    PostalCode = "44000", 
                    State = "Punjab" 
                },
                new Company
                {
                    Id = 2,
                    Name = "Ciklum",
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad",
                    PhoneNumber = "+921231234567",
                    PostalCode = "44000",
                    State = "Punjab"
                },
                new Company
                {
                    Id = 3,
                    Name = "10Pearls",
                    StreetAddress = "Gulburg Greens",
                    City = "Islamabad",
                    PhoneNumber = "+921231234567",
                    PostalCode = "44000",
                    State = "Punjab"
                }
            );
        }
    }
}
