using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSOC.Backend.Service;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace MSOC.Backend.Tests;

public class Startup
{
    // ReSharper disable once UnusedMember.Global
    public IHostBuilder CreateHostBuilder() => MinimalApiHostBuilderFactory.GetHostBuilder<Program>();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MaimaiInquiryService>();
        
        services.AddDbContext<SchoolDatabaseService>(ServiceLifetime.Transient);
        services.AddDbContext<TrackDatabaseService>(ServiceLifetime.Transient);
        services.AddDbContext<GameDatabaseService>(ServiceLifetime.Transient);

        services.ActivateSingleton<IConfiguration>();
        services.ActivateSingleton<MaimaiInquiryService>();
    }
}