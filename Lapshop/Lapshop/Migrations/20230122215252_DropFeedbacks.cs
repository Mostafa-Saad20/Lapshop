using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class DropFeedbacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
            
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
