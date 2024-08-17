using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class DatabaseServiceTest
{
    private DatabaseService _database;
    
    public DatabaseServiceTest(DatabaseService database)
    {
        _database = database;
    }

    [Fact]
    public void DatabaseInjectionWorks()
    {
        Assert.NotNull(_database);
    }
}