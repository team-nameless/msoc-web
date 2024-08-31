using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMSOCMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipLevel",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipLevel",
                table: "Players");
        }
    }
}
