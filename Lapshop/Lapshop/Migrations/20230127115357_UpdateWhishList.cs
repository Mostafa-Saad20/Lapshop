using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWhishList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Accessories_AccessoryId",
                table: "WhishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Customers_CustomerId",
                table: "WhishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Laptops_LaptopId",
                table: "WhishListItems");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "WhishListItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "WhishListItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccessoryId",
                table: "WhishListItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Accessories_AccessoryId",
                table: "WhishListItems",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Customers_CustomerId",
                table: "WhishListItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Laptops_LaptopId",
                table: "WhishListItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Accessories_AccessoryId",
                table: "WhishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Customers_CustomerId",
                table: "WhishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Laptops_LaptopId",
                table: "WhishListItems");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "WhishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "WhishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccessoryId",
                table: "WhishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Accessories_AccessoryId",
                table: "WhishListItems",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Customers_CustomerId",
                table: "WhishListItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Laptops_LaptopId",
                table: "WhishListItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
