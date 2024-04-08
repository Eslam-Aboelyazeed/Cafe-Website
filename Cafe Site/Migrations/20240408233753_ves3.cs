using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Site.Migrations
{
    /// <inheritdoc />
    public partial class ves3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_Reviews",
                table: "Product_Reviews");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Product_Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "User_Name",
                table: "Product_Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_Reviews",
                table: "Product_Reviews",
                columns: new[] { "Product_Id", "User_Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_Reviews",
                table: "Product_Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "User_Name",
                table: "Product_Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Product_Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_Reviews",
                table: "Product_Reviews",
                columns: new[] { "Product_Id", "User_Id" });
        }
    }
}
