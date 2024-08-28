#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MSOC.Backend.Migrations.TrackDatabaseServiceMigrations;

/// <inheritdoc />
public partial class AddTrackDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Tracks",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Category = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Artist = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Version = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Difficulty = table.Column<int>("INTEGER", nullable: false),
                Type = table.Column<int>("INTEGER", nullable: false),
                CoverImageUrl = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Constant = table.Column<double>("REAL", nullable: false),
                HasBeenPicked = table.Column<bool>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Tracks", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_Tracks_Title_Difficulty_Type",
            "Tracks",
            new[] { "Title", "Difficulty", "Type" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Tracks");
    }
}