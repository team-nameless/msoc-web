using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;

namespace MSOC.Backend.Service;

/// <summary>
///     A database service, for interacting with databases.
///     Meant to be used with DI.
/// </summary>
public class GameDatabaseService : DbContext
{
    public GameDatabaseService(DbContextOptions<GameDatabaseService> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Score> Scores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(
            optionsBuilder
#if DEBUG
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
#endif
        );
    }
}