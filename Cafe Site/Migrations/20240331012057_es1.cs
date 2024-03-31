using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Site.Migrations
{
    /// <inheritdoc />
    public partial class es1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Order_TotalPrice",
                table: "Orders",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<string>(
                name: "Order_Status",
                table: "Orders",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Order_Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Order_Products",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order_Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Order_Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Order_Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order_TotalPrice",
                table: "Orders",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
