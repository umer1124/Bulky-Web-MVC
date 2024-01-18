using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "CompanyId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[] { "801999c1-00c3-4a24-9127-84273dd7d267", 0, "Islamabad", null, "e692065d-79c6-41b3-ac96-be818e5e6422", "ApplicationUser", "admin@mail.com", false, true, null, "Administrator", "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEFm95zm6s8AEWYAXKqsJz9hpcVL1O/eKknXQsabPr+1XHVDg1QL0q3o3gql/+UflNQ==", "+921231234567", false, "44000", "3VXUHXJFIN2DCDKS3XYN2PI533DQQ47H", "Punjab", "Gulburg greens", false, "admin@mail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "801999c1-00c3-4a24-9127-84273dd7d267");
        }
    }
}
