using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61244749-ec1c-45fc-b3ef-9287c90491db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "801999c1-00c3-4a24-9127-84273dd7d267");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f17444f0-8643-45ef-9f4e-71f7f90b5a95");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Int = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Int);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "CompanyId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "61244749-ec1c-45fc-b3ef-9287c90491db", 0, "Islamabad", null, "d909fab9-2a74-421f-80a3-423e7a63d46c", "ApplicationUser", "customer@mail.com", false, true, null, "Customer", "CUSTOMER@MAIL.COM", "CUSTOMER@MAIL.COM", "AQAAAAIAAYagAAAAEMRda8maytGXXAtbxoKSDB28wfRgCaOgXkpIAur+G2O129pb3Ry9CTLs48ixYXu9aA==", "+921231234567", false, "44000", "F4JXHJNQUW3Y24IDGBQL57S4OMFFESUI", "Punjab", "Gulburg greens", false, "customer@mail.com" },
                    { "801999c1-00c3-4a24-9127-84273dd7d267", 0, "Islamabad", null, "e692065d-79c6-41b3-ac96-be818e5e6422", "ApplicationUser", "admin@mail.com", false, true, null, "Administrator", "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEFm95zm6s8AEWYAXKqsJz9hpcVL1O/eKknXQsabPr+1XHVDg1QL0q3o3gql/+UflNQ==", "+921231234567", false, "44000", "3VXUHXJFIN2DCDKS3XYN2PI533DQQ47H", "Punjab", "Gulburg greens", false, "admin@mail.com" },
                    { "9d43f78e-da7e-4dc1-bc26-fa3354d7f04a", 0, "Islamabad", null, "7287cd7b-087c-49c1-afda-257446473577", "ApplicationUser", "employee@mail.com", false, true, null, "Employee", "EMPLOYEE@MAIL.COM", "EMPLOYEE@MAIL.COM", "AQAAAAIAAYagAAAAECk8dlZBCbwCFCh2NUtIfvSSnbf2i6OSV2MBcoRm0xQCnFl5HRyYIK7DMWlqicuuPg==", "+921231234567", false, "44000", "B3VW3NGJPLJIB7VAJPHE4ZQXQRXNG5GT", "Punjab", "Gulburg greens", false, "employee@mail.com" },
                    { "f17444f0-8643-45ef-9f4e-71f7f90b5a95", 0, "Islamabad", 1, "9913189b-ff1e-45cb-b841-b4b3af24d48e", "ApplicationUser", "company@mail.com", false, true, null, "Company", "COMPANY@MAIL.COM", "COMPANY@MAIL.COM", "AQAAAAIAAYagAAAAEM7Hz2KjQMFZkx9nAqCZivwRQVatNIWAdMDRBHmOWR1ktJBIQ+AFOQFI+RcsFIMXCQ==", "+921231234567", false, "44000", "RYDPS4RCBJB6H6YC6O7OIN7JDGXOTMI7", "Punjab", "Gulburg greens", false, "company@mail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "\\images\\product\\f8bcff4f-3fb2-4b86-99b8-9eeed58e70fe.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "\\images\\product\\0151bb54-ad86-41e8-acae-9448e2019d31.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "\\images\\product\\293b3549-a885-41b8-b4d7-7a184d642c70.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "\\images\\product\\d9440830-a555-4835-b64e-16a31bd0c755.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "\\images\\product\\e1be1146-e49c-4857-a3e2-ba5717114a1c.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "\\images\\product\\ef2f64ef-4eb3-4c96-a87c-5310ae658a61.jpg");

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
    }
}
