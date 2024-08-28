using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSOC.Backend.Migrations.TrackDatabaseServiceMigrations
{
    /// <inheritdoc />
    public partial class AddTrackDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Artist = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Constant = table.Column<double>(type: "REAL", nullable: false),
                    HasBeenPicked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_Title_Difficulty_Type",
                table: "Tracks",
                columns: new[] { "Title", "Difficulty", "Type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
