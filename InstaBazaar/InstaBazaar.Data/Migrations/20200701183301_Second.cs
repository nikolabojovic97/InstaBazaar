using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaBazaar.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "569ff0e3-9846-4ce8-aba8-a26e7ca18b41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2ab2dd0-452c-4f55-9f15-55b4170a6571");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca314fd7-0ad9-4cf7-b020-586268ece234");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f1f86f6-4ee9-430a-83b8-750110ae868c", "ba69ed0d-e6b8-4348-b64b-1de2ee460302", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e69aa02-64dc-4e39-9bd4-cca6c2183bc1", "666582b5-4678-4339-9b75-09cbdce4420e", "Brand", "BRAND" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc2748d4-88e2-43c3-bde3-c148ddfa4b09", "ed7fffd8-080e-4546-a93f-3adbd6a03ead", "Influencer", "INFLUENCER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e69aa02-64dc-4e39-9bd4-cca6c2183bc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f1f86f6-4ee9-430a-83b8-750110ae868c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc2748d4-88e2-43c3-bde3-c148ddfa4b09");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "569ff0e3-9846-4ce8-aba8-a26e7ca18b41", "19b39ce5-52d4-4456-8484-3291de70253a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca314fd7-0ad9-4cf7-b020-586268ece234", "a1881e5a-329b-4848-aa9c-0189c77b15cb", "Brand", "BRAND" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2ab2dd0-452c-4f55-9f15-55b4170a6571", "9d74c4ac-8f5f-4c1c-8aaf-009433ab7032", "Influencer", "INFLUENCER" });
        }
    }
}
