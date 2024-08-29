using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Service;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace MSOC.Backend.Tests;

public class BackendTestBedFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        var path = Directory.CreateDirectory(AppContext.BaseDirectory)
            .Parent!.Parent!.Parent!.Parent!.ToString();
        
        services.AddSingleton(configuration!);
        services.AddSingleton<MaimaiInquiryService>();

        services.AddDbContext<SchoolDatabaseService>(
            o => o.UseSqlite($"Filename={path}/MSOC.Backend/schools.db"),
            ServiceLifetime.Transient
        );
        
        services.AddDbContext<TrackDatabaseService>(
            o => o.UseSqlite($"Filename={path}/MSOC.Backend/tracks.db"),
            ServiceLifetime.Transient
        );

        services.AddDbContext<GameDatabaseService>(
            o => o.UseSqlite($"Filename=:memory:"),
            ServiceLifetime.Transient
        );
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        var path = Directory.CreateDirectory(AppContext.BaseDirectory)
            .Parent!.Parent!.Parent!.Parent!.ToString();
        
        yield return new TestAppSettings
        {
            Filename = $"{path}/MSOC.Backend/config.json",
            IsOptional = false
        };
    }

    protected override ValueTask DisposeAsyncCore() => new();
    
    protected override void AddUserSecrets(IConfigurationBuilder configurationBuilder) 
        => configurationBuilder.AddUserSecrets<BackendTestBedFixture>();
}