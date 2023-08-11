using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouponApi.Migrations
{
    public partial class seedcoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "Discount", "MinAmount" },
                values: new object[] { 1, "10OFF", 10.0, 10 });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "Discount", "MinAmount" },
                values: new object[] { 2, "20OFF", 20.0, 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
