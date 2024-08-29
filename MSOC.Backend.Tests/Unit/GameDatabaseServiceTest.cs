using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class GameDatabaseServiceTest
{
    private readonly GameDatabaseService _gameDatabase;

    public GameDatabaseServiceTest(GameDatabaseService gameDatabase)
    {
        _gameDatabase = gameDatabase;
    }

    [Fact]
    public void DatabaseInjectionWorks()
    {
        Assert.NotNull(_gameDatabase);
    }
}