using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;

namespace MSOC.Backend.Service;

/// <summary>
///     A school database, well, for interacting with schools.
///     Meant to be used with DI.
/// </summary>
public class SchoolDatabaseService : DbContext
{
    public DbSet<School> Schools { get; set; }
    
    public SchoolDatabaseService(DbContextOptions<SchoolDatabaseService> options) : base(options)
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
                .UseSqlite(new SqliteConnection("Filename=schools.db;"))
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>()
            .Property(school => school.Type)
            .HasConversion<int>();
    }
}