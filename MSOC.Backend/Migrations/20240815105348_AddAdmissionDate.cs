using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAdmission",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfAdmission",
                table: "Players");
        }
    }
}
