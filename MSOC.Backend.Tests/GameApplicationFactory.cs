using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests;

public class GameApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var gameDatabase = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GameDatabaseService>));
            var trackDatabase = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TrackDatabaseService>));
            var schoolDatabase = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SchoolDatabaseService>));
            
            if (gameDatabase != null) services.Remove(gameDatabase);
            if (trackDatabase != null) services.Remove(trackDatabase);
            if (schoolDatabase != null) services.Remove(schoolDatabase);
            
            var path = Directory.CreateDirectory(AppContext.BaseDirectory)
                .Parent!.Parent!.Parent!.Parent!.ToString();
        
            services.AddDbContext<SchoolDatabaseService>(
                o => o.UseSqlite($"Filename={path}/MSOC.Backend/schools.db"),
                ServiceLifetime.Transient
            );
        
            services.AddDbContext<TrackDatabaseService>(
                o => o.UseSqlite($"Filename={path}/MSOC.Backend/tracks.db"),
                ServiceLifetime.Transient
            );

            services.AddDbContext<GameDatabaseService>(
                o => o.UseSqlite($"Filename={path}/MSOC.Backend/MSOC.Test.db"),
                ServiceLifetime.Transient
            );

            // services.AddSingleton<IConfiguration>();
            services.AddTransient<MaimaiInquiryService>();
        });

        return base.CreateHost(builder);
    }
}