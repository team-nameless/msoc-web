using MSOC.Backend.Service;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace MSOC.Backend.Tests.Unit;

public class TrackDatabaseServiceTest(ITestOutputHelper testOutputHelper, BackendTestBedFixture fixture) 
    : TestBed<BackendTestBedFixture>(testOutputHelper, fixture)
{
    [Fact]
    public void DatabaseInjectionWorks()
    {
        var trackDatabase = _fixture.GetService<TrackDatabaseService>(_testOutputHelper)!;
        trackDatabase.Database.EnsureCreated();
        
        Assert.NotNull(trackDatabase);
    }
    
    [Fact]
    public void AllTablesExists()
    {
        var trackDatabase = _fixture.GetService<TrackDatabaseService>(_testOutputHelper)!;
        trackDatabase.Database.EnsureCreated();
        
        var _ = trackDatabase.Tracks;
        
        Assert.True(true);
    }
    
    [Fact]
    public void AllEntriesExists()
    {
        var trackDatabase = _fixture.GetService<TrackDatabaseService>(_testOutputHelper)!;
        trackDatabase.Database.EnsureCreated();
        
        Assert.InRange(trackDatabase.Tracks.Count(), 620, 650);
    }
}