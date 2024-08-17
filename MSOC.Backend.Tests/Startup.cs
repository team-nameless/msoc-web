using MSOC.Backend.Service;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace MSOC.Backend.Tests;

public class Startup
{
    [JetBrains.Annotations.UsedImplicitly]
    public IHostBuilder CreateHostBuilder() => MinimalApiHostBuilderFactory.GetHostBuilder<Program>();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MaimaiInquiryService>();
        services.AddDbContext<DatabaseService>(ServiceLifetime.Transient);

        services.ActivateSingleton<IConfiguration>();
        services.ActivateSingleton<MaimaiInquiryService>();
    }
}