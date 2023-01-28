using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class CreateComparisonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Comparisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaptopId = table.Column<int>(type: "int", nullable: true),
                    AccessoryId = table.Column<int>(type: "int", nullable: true),
                    UniqueProperty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparisons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comparisons");

            migrationBuilder.CreateTable(
                name: "Comparsions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstAccessoryId = table.Column<int>(type: "int", nullable: true),
                    FirstLaptopId = table.Column<int>(type: "int", nullable: true),
                    SecondAccessoryId = table.Column<int>(type: "int", nullable: true),
                    SecondLaptopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparsions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comparsions_Accessories_FirstAccessoryId",
                        column: x => x.FirstAccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comparsions_Accessories_SecondAccessoryId",
                        column: x => x.SecondAccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comparsions_Laptops_FirstLaptopId",
                        column: x => x.FirstLaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comparsions_Laptops_SecondLaptopId",
                        column: x => x.SecondLaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comparsions_FirstAccessoryId",
                table: "Comparsions",
                column: "FirstAccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparsions_FirstLaptopId",
                table: "Comparsions",
                column: "FirstLaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparsions_SecondAccessoryId",
                table: "Comparsions",
                column: "SecondAccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparsions_SecondLaptopId",
                table: "Comparsions",
                column: "SecondLaptopId");
        }
    }
}
