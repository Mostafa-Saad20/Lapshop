using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableReviews2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropIndex(
				name: "IX_AccessoryReviews_AccessoryId",
                table: "AccessoryReviews");

			migrationBuilder.DropIndex(
				name: "IX_LaptopReviews_LaptopId"
                ,table: "LaptopReviews");

			migrationBuilder.DropForeignKey(
				name: "FK_AccessoryReviews_Accessories_AccessoryId",
				table: "AccessoryReviews");

			migrationBuilder.DropForeignKey(
				name: "FK_LaptopReviews_Laptops_LaptopId",
				table: "LaptopReviews");

			migrationBuilder.DropColumn(
				name: "AccessoryId",
				table: "AccessoryReviews");

			migrationBuilder.DropColumn(
				name: "LaptopId",
				table: "LaptopReviews");

			migrationBuilder.AddColumn<int>(
                name: "LapId",
                table: "LaptopReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccId",
                table: "AccessoryReviews",
                type: "int",
                nullable: true);
	
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LapId",
                table: "LaptopReviews");

            migrationBuilder.DropColumn(
                name: "AccId",
                table: "AccessoryReviews");
        }
    }
}
