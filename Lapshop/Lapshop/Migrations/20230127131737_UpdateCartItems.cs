using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Customers_CustomerId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_AccessoryId",
                table: "CartItems",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_LaptopId",
                table: "CartItems",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Accessories_AccessoryId",
                table: "CartItems",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Customers_CustomerId",
                table: "CartItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Laptops_LaptopId",
                table: "CartItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Accessories_AccessoryId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Customers_CustomerId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Laptops_LaptopId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_AccessoryId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_LaptopId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "AccessoryId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Customers_CustomerId",
                table: "CartItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
