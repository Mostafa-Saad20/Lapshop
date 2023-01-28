using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class CreateComparisionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Comparsions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLaptopId = table.Column<int>(type: "int", nullable: true),
                    SecondLaptopId = table.Column<int>(type: "int", nullable: true),
                    FirstAccessoryId = table.Column<int>(type: "int", nullable: true),
                    SecondAccessoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparsions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comparsions");

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "LaptopReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "AccessoryReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LaptopReviews_LaptopId",
                table: "LaptopReviews",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessoryReviews_AccessoryId",
                table: "AccessoryReviews",
                column: "AccessoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryReviews_Accessories_AccessoryId",
                table: "AccessoryReviews",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopReviews_Laptops_LaptopId",
                table: "LaptopReviews",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id");
        }
    }
}
