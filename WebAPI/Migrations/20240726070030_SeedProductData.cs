using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class SeedProductData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products4",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Name", "Price", "Stock" },
                values: new object[] { 1, new DateTime(2024, 7, 23, 10, 0, 29, 697, DateTimeKind.Local).AddTicks(4902), null, "Bilgisayar", 15000m, 300 });

            migrationBuilder.InsertData(
                table: "Products4",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Name", "Price", "Stock" },
                values: new object[] { 2, new DateTime(2024, 6, 26, 10, 0, 29, 701, DateTimeKind.Local).AddTicks(752), null, "Telefon", 10000m, 500 });

            migrationBuilder.InsertData(
                table: "Products4",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Name", "Price", "Stock" },
                values: new object[] { 3, new DateTime(2024, 5, 27, 10, 0, 29, 701, DateTimeKind.Local).AddTicks(785), null, "Klavye", 1000m, 1000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products4",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products4",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products4",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
