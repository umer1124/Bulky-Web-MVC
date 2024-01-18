using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersSeedDataFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "CompanyId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "61244749-ec1c-45fc-b3ef-9287c90491db", 0, "Islamabad", null, "d909fab9-2a74-421f-80a3-423e7a63d46c", "ApplicationUser", "customer@mail.com", false, true, null, "Customer", "CUSTOMER@MAIL.COM", "CUSTOMER@MAIL.COM", "AQAAAAIAAYagAAAAEMRda8maytGXXAtbxoKSDB28wfRgCaOgXkpIAur+G2O129pb3Ry9CTLs48ixYXu9aA==", "+921231234567", false, "44000", "F4JXHJNQUW3Y24IDGBQL57S4OMFFESUI", "Punjab", "Gulburg greens", false, "customer@mail.com" },
                    { "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a", 0, "Islamabad", null, "7287cd7b-087c-49c1-afda-257446473577", "ApplicationUser", "employee@mail.com", false, true, null, "Employee", "EMPLOYEE@MAIL.COM", "EMPLOYEE@MAIL.COM", "AQAAAAIAAYagAAAAECk8dlZBCbwCFCh2NUtIfvSSnbf2i6OSV2MBcoRm0xQCnFl5HRyYIK7DMWlqicuuPg==", "+921231234567", false, "44000", "B3VW3NGJPLJIB7VAJPHE4ZQXQRXNG5GT", "Punjab", "Gulburg greens", false, "employee@mail.com" },
                    { "f17444f0-8643-45ef-9f4e-71f7f90b5a95", 0, "Islamabad", 1, "9913189b-ff1e-45cb-b841-b4b3af24d48e", "ApplicationUser", "company@mail.com", false, true, null, "Company", "COMPANY@MAIL.COM", "COMPANY@MAIL.COM", "AQAAAAIAAYagAAAAEM7Hz2KjQMFZkx9nAqCZivwRQVatNIWAdMDRBHmOWR1ktJBIQ+AFOQFI+RcsFIMXCQ==", "+921231234567", false, "44000", "RYDPS4RCBJB6H6YC6O7OIN7JDGXOTMI7", "Punjab", "Gulburg greens", false, "company@mail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61244749-ec1c-45fc-b3ef-9287c90491db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f17444f0-8643-45ef-9f4e-71f7f90b5a95");
        }
    }
}
