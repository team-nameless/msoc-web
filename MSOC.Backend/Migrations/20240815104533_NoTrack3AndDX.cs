using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class NoTrack3AndDX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sub3",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TotalDeluxeScore",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Sub3",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "TotalDeluxeScore",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
