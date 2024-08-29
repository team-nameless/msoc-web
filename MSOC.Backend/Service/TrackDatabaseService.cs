using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;

namespace MSOC.Backend.Service;

/// <summary>
///     A track database, well, for interacting with tracks.
///     Meant to be used with DI.
/// </summary>
public class TrackDatabaseService : DbContext
{
    public TrackDatabaseService(DbContextOptions<TrackDatabaseService> options) : base(options)
    {
    }
    
    public DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(
            optionsBuilder
#if DEBUG
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
#endif
                .UseSqlite(new SqliteConnection("Data Source=tracks.db;"))
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>()
            .Property(track => track.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Track>()
            .Property(track => track.Difficulty)
            .HasConversion<int>();

        modelBuilder.Entity<Track>()
            .Property(track => track.Type)
            .HasConversion<int>();
    }
}