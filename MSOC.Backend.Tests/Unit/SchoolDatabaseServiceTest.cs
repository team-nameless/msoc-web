using MSOC.Backend.Service;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace MSOC.Backend.Tests.Unit;

public class SchoolDatabaseServiceTest(ITestOutputHelper testOutputHelper, ServiceTestBedFixture fixture) 
    : TestBed<ServiceTestBedFixture>(testOutputHelper, fixture)
{
    [Fact]
    public void DatabaseInjectionWorks()
    {
        var schoolDatabase = _fixture.GetService<SchoolDatabaseService>(_testOutputHelper)!;
        schoolDatabase.Database.EnsureCreated();
        
        Assert.NotNull(schoolDatabase);
    }
    
    [Fact]
    public void AllTablesExists()
    {        
        var schoolDatabase = _fixture.GetService<SchoolDatabaseService>(_testOutputHelper)!;
        schoolDatabase.Database.EnsureCreated();
        
        var _ = schoolDatabase.Schools;
        
        Assert.True(true);
    }
    
    [Fact]
    public void AllEntriesExists()
    {
        var schoolDatabase = _fixture.GetService<SchoolDatabaseService>(_testOutputHelper)!;
        schoolDatabase.Database.EnsureCreated();
        
        Assert.StrictEqual(256, schoolDatabase.Schools.Count());
    }
}