using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapshop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComparisonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniqueProperty",
                table: "Comparisons",
                newName: "RAMType");

            migrationBuilder.AddColumn<decimal>(
                name: "CPU",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DisplaySize",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GPU",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GPUBrand",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HDD",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSSD",
                table: "Comparisons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProcessorBrand",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessorType",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RAMSize",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SSD",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Comparisons",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPU",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "DisplaySize",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "GPU",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "GPUBrand",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "HDD",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "HasSSD",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "ProcessorBrand",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "ProcessorType",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "RAMSize",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "SSD",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Comparisons");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Comparisons");

            migrationBuilder.RenameColumn(
                name: "RAMType",
                table: "Comparisons",
                newName: "UniqueProperty");
        }
    }
}
