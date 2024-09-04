using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ReintroduceDiscordId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "FriendCode",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendCode",
                table: "Players");
        }
    }
}
