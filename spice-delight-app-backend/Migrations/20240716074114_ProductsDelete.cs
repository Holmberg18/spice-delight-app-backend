using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spice_delight_app_backend.Migrations
{
    /// <inheritdoc />
    public partial class ProductsDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IdMeal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FastDelivery = table.Column<bool>(type: "bit", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ratings = table.Column<int>(type: "int", nullable: false),
                    StrCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrMeal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrMealThumb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IdMeal);
                });
        }
    }
}
