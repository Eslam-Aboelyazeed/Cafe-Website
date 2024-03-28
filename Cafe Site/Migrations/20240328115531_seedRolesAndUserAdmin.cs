using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Site.Migrations
{
    /// <inheritdoc />
    public partial class seedRolesAndUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "AspNetRoles",
       columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
       values: new object[] { "0d48cc70-46e4-4fb6-998a-7fbf96c0955b", "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString()}
       );
            migrationBuilder.InsertData(
                   table: "AspNetRoles",
                   columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                   values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
