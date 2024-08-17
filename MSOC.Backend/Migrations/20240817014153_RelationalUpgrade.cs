using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RelationalUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Schools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_TeamId",
                table: "Schools",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Teams_TeamId",
                table: "Schools",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Teams_TeamId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_TeamId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Schools");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
