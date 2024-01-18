using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRolesMappingSeedDataInDbContextClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "803ee787-bbdc-44e7-8a22-9976391b3182", "61244749-ec1c-45fc-b3ef-9287c90491db" },
                    { "137c8e30-8011-4e13-835a-9ed6056b09a6", "801999c1-00c3-4a24-9127-84273dd7d267" },
                    { "3c09f235-e3a2-4530-82e2-257b1f6ead6c", "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a" },
                    { "2e6eb075-8360-4354-b40b-29f257b319b1", "f17444f0-8643-45ef-9f4e-71f7f90b5a95" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "803ee787-bbdc-44e7-8a22-9976391b3182", "61244749-ec1c-45fc-b3ef-9287c90491db" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "137c8e30-8011-4e13-835a-9ed6056b09a6", "801999c1-00c3-4a24-9127-84273dd7d267" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3c09f235-e3a2-4530-82e2-257b1f6ead6c", "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e6eb075-8360-4354-b40b-29f257b319b1", "f17444f0-8643-45ef-9f4e-71f7f90b5a95" });
        }
    }
}
