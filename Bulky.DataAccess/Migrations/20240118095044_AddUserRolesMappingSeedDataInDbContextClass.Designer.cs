﻿// <auto-generated />
using System;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bulky.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240118095044_AddUserRolesMappingSeedDataInDbContextClass")]
    partial class AddUserRolesMappingSeedDataInDbContextClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bulky.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "SciFi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Islamabad",
                            Name = "TeraData",
                            PhoneNumber = "+921231234567",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg Greens"
                        },
                        new
                        {
                            Id = 2,
                            City = "Islamabad",
                            Name = "Ciklum",
                            PhoneNumber = "+921231234567",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg Greens"
                        },
                        new
                        {
                            Id = 3,
                            City = "Islamabad",
                            Name = "10Pearls",
                            PhoneNumber = "+921231234567",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg Greens"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Billy Spark",
                            CategoryId = 1,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "SWD9999001",
                            ImageUrl = "\\images\\product\\f8bcff4f-3fb2-4b86-99b8-9eeed58e70fe.jpg",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Fortune of Time"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Nancy Hoover",
                            CategoryId = 1,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "CAW777777701",
                            ImageUrl = "\\images\\product\\0151bb54-ad86-41e8-acae-9448e2019d31.jpg",
                            ListPrice = 40.0,
                            Price = 30.0,
                            Price100 = 20.0,
                            Price50 = 25.0,
                            Title = "Dark Skies"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Julian Button",
                            CategoryId = 1,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "RITO5555501",
                            ImageUrl = "\\images\\product\\293b3549-a885-41b8-b4d7-7a184d642c70.jpg",
                            ListPrice = 55.0,
                            Price = 50.0,
                            Price100 = 35.0,
                            Price50 = 40.0,
                            Title = "Vanish in the Sunset"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Abby Muscles",
                            CategoryId = 2,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "WS3333333301",
                            ImageUrl = "\\images\\product\\d9440830-a555-4835-b64e-16a31bd0c755.jpg",
                            ListPrice = 70.0,
                            Price = 65.0,
                            Price100 = 55.0,
                            Price50 = 60.0,
                            Title = "Cotton Candy"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Ron Parker",
                            CategoryId = 2,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "SOTJ1111111101",
                            ImageUrl = "\\images\\product\\e1be1146-e49c-4857-a3e2-ba5717114a1c.jpg",
                            ListPrice = 30.0,
                            Price = 27.0,
                            Price100 = 20.0,
                            Price50 = 25.0,
                            Title = "Rock in the Ocean"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Laura Phantom",
                            CategoryId = 3,
                            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                            ISBN = "FOT000000001",
                            ImageUrl = "\\images\\product\\ef2f64ef-4eb3-4c96-a87c-5310ae658a61.jpg",
                            ListPrice = 25.0,
                            Price = 23.0,
                            Price100 = 20.0,
                            Price50 = 22.0,
                            Title = "Leaves and Wonders"
                        });
                });

            modelBuilder.Entity("Bulky.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "137c8e30-8011-4e13-835a-9ed6056b09a6",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2e6eb075-8360-4354-b40b-29f257b319b1",
                            Name = "Company",
                            NormalizedName = "COMPANY"
                        },
                        new
                        {
                            Id = "3c09f235-e3a2-4530-82e2-257b1f6ead6c",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "803ee787-bbdc-44e7-8a22-9976391b3182",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "801999c1-00c3-4a24-9127-84273dd7d267",
                            RoleId = "137c8e30-8011-4e13-835a-9ed6056b09a6"
                        },
                        new
                        {
                            UserId = "f17444f0-8643-45ef-9f4e-71f7f90b5a95",
                            RoleId = "2e6eb075-8360-4354-b40b-29f257b319b1"
                        },
                        new
                        {
                            UserId = "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a",
                            RoleId = "3c09f235-e3a2-4530-82e2-257b1f6ead6c"
                        },
                        new
                        {
                            UserId = "61244749-ec1c-45fc-b3ef-9287c90491db",
                            RoleId = "803ee787-bbdc-44e7-8a22-9976391b3182"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Bulky.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CompanyId");

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "801999c1-00c3-4a24-9127-84273dd7d267",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e692065d-79c6-41b3-ac96-be818e5e6422",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEFm95zm6s8AEWYAXKqsJz9hpcVL1O/eKknXQsabPr+1XHVDg1QL0q3o3gql/+UflNQ==",
                            PhoneNumber = "+921231234567",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3VXUHXJFIN2DCDKS3XYN2PI533DQQ47H",
                            TwoFactorEnabled = false,
                            UserName = "admin@mail.com",
                            City = "Islamabad",
                            Name = "Administrator",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg greens"
                        },
                        new
                        {
                            Id = "f17444f0-8643-45ef-9f4e-71f7f90b5a95",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9913189b-ff1e-45cb-b841-b4b3af24d48e",
                            Email = "company@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            NormalizedEmail = "COMPANY@MAIL.COM",
                            NormalizedUserName = "COMPANY@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEM7Hz2KjQMFZkx9nAqCZivwRQVatNIWAdMDRBHmOWR1ktJBIQ+AFOQFI+RcsFIMXCQ==",
                            PhoneNumber = "+921231234567",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "RYDPS4RCBJB6H6YC6O7OIN7JDGXOTMI7",
                            TwoFactorEnabled = false,
                            UserName = "company@mail.com",
                            City = "Islamabad",
                            CompanyId = 1,
                            Name = "Company",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg greens"
                        },
                        new
                        {
                            Id = "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7287cd7b-087c-49c1-afda-257446473577",
                            Email = "employee@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            NormalizedEmail = "EMPLOYEE@MAIL.COM",
                            NormalizedUserName = "EMPLOYEE@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAECk8dlZBCbwCFCh2NUtIfvSSnbf2i6OSV2MBcoRm0xQCnFl5HRyYIK7DMWlqicuuPg==",
                            PhoneNumber = "+921231234567",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "B3VW3NGJPLJIB7VAJPHE4ZQXQRXNG5GT",
                            TwoFactorEnabled = false,
                            UserName = "employee@mail.com",
                            City = "Islamabad",
                            Name = "Employee",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg greens"
                        },
                        new
                        {
                            Id = "61244749-ec1c-45fc-b3ef-9287c90491db",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d909fab9-2a74-421f-80a3-423e7a63d46c",
                            Email = "customer@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            NormalizedEmail = "CUSTOMER@MAIL.COM",
                            NormalizedUserName = "CUSTOMER@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEMRda8maytGXXAtbxoKSDB28wfRgCaOgXkpIAur+G2O129pb3Ry9CTLs48ixYXu9aA==",
                            PhoneNumber = "+921231234567",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "F4JXHJNQUW3Y24IDGBQL57S4OMFFESUI",
                            TwoFactorEnabled = false,
                            UserName = "customer@mail.com",
                            City = "Islamabad",
                            Name = "Customer",
                            PostalCode = "44000",
                            State = "Punjab",
                            StreetAddress = "Gulburg greens"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.HasOne("Bulky.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bulky.Models.ShoppingCart", b =>
                {
                    b.HasOne("Bulky.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bulky.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bulky.Models.ApplicationUser", b =>
                {
                    b.HasOne("Bulky.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
