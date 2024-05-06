using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umami.Migrations
{
    /// <inheritdoc />
    public partial class v021addedcookingamountsofproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductAmounts",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAmounts",
                table: "Recipe");
        }
    }
}
