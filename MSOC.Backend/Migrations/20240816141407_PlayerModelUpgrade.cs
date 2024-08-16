using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class PlayerModelUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Schools");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Schools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Schools");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Schools",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
