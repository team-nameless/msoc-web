using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;

namespace MSOC.Backend.Service;

/// <summary>
///     A database service, for interacting with databases.
///     Meant to be used with DI.
/// </summary>
public class GameDatabaseService : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Score> Scores { get; set; }
    
    public GameDatabaseService(DbContextOptions<GameDatabaseService> options) : base(options)
    {
    }

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