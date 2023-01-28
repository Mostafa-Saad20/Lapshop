using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Feedbacks",
                newName: "Review");

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "Feedbacks",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "Feedbacks",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AccessoryId",
                table: "Feedbacks",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_LaptopId",
                table: "Feedbacks",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Accessories_AccessoryId",
                table: "Feedbacks",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Laptops_LaptopId",
                table: "Feedbacks",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Accessories_AccessoryId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Laptops_LaptopId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AccessoryId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_LaptopId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "AccessoryId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Feedbacks",
                newName: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
