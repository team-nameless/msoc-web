using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;

namespace MSOC.Backend.Service;

public class DatabaseService : DbContext
{
    public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<School> Schools { get; set; }
}