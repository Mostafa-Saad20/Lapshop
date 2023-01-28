using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesForImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccId = table.Column<int>(type: "int", nullable: false),
                    AccessoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessoryImages_Accessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LaptopImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LapId = table.Column<int>(type: "int", nullable: false),
                    LaptopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaptopImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaptopImages_Laptops_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessoryImages_AccessoryId",
                table: "AccessoryImages",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LaptopImages_LaptopId",
                table: "LaptopImages",
                column: "LaptopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessoryImages");

            migrationBuilder.DropTable(
                name: "LaptopImages");
        }
    }
}
