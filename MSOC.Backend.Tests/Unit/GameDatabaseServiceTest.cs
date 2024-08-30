using MSOC.Backend.Service;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace MSOC.Backend.Tests.Unit;

public class GameDatabaseServiceTest(ITestOutputHelper testOutputHelper, ServiceTestBedFixture fixture) 
    : TestBed<ServiceTestBedFixture>(testOutputHelper, fixture)
{
    [Fact]
    public void DatabaseInjectionWorks()
    {
        var gameDatabase = _fixture.GetService<GameDatabaseService>(_testOutputHelper)!;
        gameDatabase.Database.EnsureCreated();
        
        Assert.NotNull(gameDatabase);
    }

    [Fact]
    public void AllTablesExists()
    {
        var gameDatabase = _fixture.GetService<GameDatabaseService>(_testOutputHelper)!;
        gameDatabase.Database.EnsureCreated();
        
        var players = gameDatabase.Players;
        var teams = gameDatabase.Teams;
        var scores = gameDatabase.Scores;
        
        Assert.True(true);
    }
}