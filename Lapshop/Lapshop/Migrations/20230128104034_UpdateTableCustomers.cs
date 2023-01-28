using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMessages_Customers_CustomerId",
                table: "CustomerMessages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerMessages_CustomerId",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMessages_CustomerId",
                table: "CustomerMessages",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMessages_Customers_CustomerId",
                table: "CustomerMessages",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
