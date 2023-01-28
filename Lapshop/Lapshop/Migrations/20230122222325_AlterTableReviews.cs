using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryReviews_Accessories_AccessoryId",
                table: "AccessoryReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopReviews_Laptops_LaptopId",
                table: "LaptopReviews");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "LaptopReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccessoryId",
                table: "AccessoryReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessoryReviews_Accessories_AccessoryId",
                table: "AccessoryReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopReviews_Laptops_LaptopId",
                table: "LaptopReviews");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "LaptopReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccessoryId",
                table: "AccessoryReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessoryReviews_Accessories_AccessoryId",
                table: "AccessoryReviews",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopReviews_Laptops_LaptopId",
                table: "LaptopReviews",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
