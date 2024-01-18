using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "137c8e30-8011-4e13-835a-9ed6056b09a6", null, "Admin", "ADMIN" },
                    { "2e6eb075-8360-4354-b40b-29f257b319b1", null, "Company", "COMPANY" },
                    { "3c09f235-e3a2-4530-82e2-257b1f6ead6c", null, "Employee", "EMPLOYEE" },
                    { "803ee787-bbdc-44e7-8a22-9976391b3182", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "137c8e30-8011-4e13-835a-9ed6056b09a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e6eb075-8360-4354-b40b-29f257b319b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c09f235-e3a2-4530-82e2-257b1f6ead6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "803ee787-bbdc-44e7-8a22-9976391b3182");
        }
    }
}
