using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class SchoolDatabaseServiceTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;

    public SchoolDatabaseServiceTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void DatabaseInjectionWorks()
    {
        using var scope = _factory.Services.CreateScope();
        var schoolDatabase = scope.ServiceProvider.GetService<SchoolDatabaseService>()!;
        
        schoolDatabase.Database.EnsureCreated();
        
        Assert.NotNull(schoolDatabase);
    }
    
    [Fact]
    public void AllTablesExists()
    {
        using var scope = _factory.Services.CreateScope();
        var schoolDatabase = scope.ServiceProvider.GetService<SchoolDatabaseService>()!;
        
        var _ = schoolDatabase.Schools;
        
        Assert.True(true);
    }
    
    [Fact]
    public void AllEntriesExists()
    {
        using var scope = _factory.Services.CreateScope();
        var schoolDatabase = scope.ServiceProvider.GetService<SchoolDatabaseService>()!;
        
        Assert.StrictEqual(256, schoolDatabase.Schools.Count());
    }
}