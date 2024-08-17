using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class PlayerNoLongerUseSchoolId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_SchoolId",
                table: "Players",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Schools_SchoolId",
                table: "Players",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Schools_SchoolId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_SchoolId",
                table: "Players");
        }
    }
}
