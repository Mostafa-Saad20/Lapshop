using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableAndAlterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Image",
				table: "Laptops",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.CreateTable(
                name: "CustomerMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerMessages_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMessages_CustomerId",
                table: "CustomerMessages",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Accessories");
        }
    }
}
