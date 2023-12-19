using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class SeededDefaultUserandRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2380b3f3-5150-4bfc-8a15-1bce86ee6781", "1f06c939-a249-4cc6-bf23-1125aa19db1f", "User", "USER" },
                    { "62bf172a-22c1-4068-ba80-e0346921565d", "eca8a414-6d7b-4dc7-be09-1b60096ff0fe", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2aab5595-65a6-470a-be6f-10779f5f9b7b", 0, "dc9bf70e-e166-4e4a-acba-55c2ec0a1941", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEN2xkDC7RjXZYEDsbdoEO9YLYrCbNh5gQkBjNOa8A+hGekiAOsKuMDObc4kIfTVe/A==", null, false, "156b152d-b00a-436c-8a5b-822c2057ad32", false, "user@bookstore.com" },
                    { "d145be23-6977-4b7d-b1bf-4395ec8a3c13", 0, "680ab850-0f4e-4c27-859d-f72ac1b83652", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEHsTEHWoQBAhuAeQQc5HP450SvZvALg6On0tbyXmb//quPXwZ6vsxw7sYe6TdHsrIA==", null, false, "9a4d53d8-fa1c-4e1c-a456-f9b513883e21", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2380b3f3-5150-4bfc-8a15-1bce86ee6781", "2aab5595-65a6-470a-be6f-10779f5f9b7b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "62bf172a-22c1-4068-ba80-e0346921565d", "d145be23-6977-4b7d-b1bf-4395ec8a3c13" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2380b3f3-5150-4bfc-8a15-1bce86ee6781", "2aab5595-65a6-470a-be6f-10779f5f9b7b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "62bf172a-22c1-4068-ba80-e0346921565d", "d145be23-6977-4b7d-b1bf-4395ec8a3c13" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2380b3f3-5150-4bfc-8a15-1bce86ee6781");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62bf172a-22c1-4068-ba80-e0346921565d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aab5595-65a6-470a-be6f-10779f5f9b7b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d145be23-6977-4b7d-b1bf-4395ec8a3c13");
        }
    }
}
