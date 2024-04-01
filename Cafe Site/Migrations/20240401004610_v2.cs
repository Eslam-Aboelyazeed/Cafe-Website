using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Site.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Products",
                table: "Order_Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Products",
                table: "Order_Products",
                columns: new[] { "Product_Id", "Order_Id", "Size" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Products",
                table: "Order_Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Products",
                table: "Order_Products",
                columns: new[] { "Product_Id", "Order_Id" });
        }
    }
}
