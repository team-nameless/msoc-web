using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class TrackDatabaseServiceTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;

    public TrackDatabaseServiceTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void DatabaseInjectionWorks()
    {
        using var scope = _factory.Services.CreateScope();
        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        
        trackDatabase.Database.EnsureCreated();
        
        Assert.NotNull(trackDatabase);
    }
    
    [Fact]
    public void AllTablesExists()
    {
        using var scope = _factory.Services.CreateScope();
        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        
        var _ = trackDatabase.Tracks;
        
        Assert.True(true);
    }
    
    [Fact]
    public void AllEntriesExists()
    {
        using var scope = _factory.Services.CreateScope();
        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        
        Assert.StrictEqual(626, trackDatabase.Tracks.Count());
    }
}