using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class AlterFeedbacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropIndex(
				name: "IX_Feedbacks_CustomerId",
				table: "Feedbacks");
            
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Feedbacks");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
