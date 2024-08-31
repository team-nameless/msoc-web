using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Service;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace MSOC.Backend.Tests.Integration;

public class AdminControllerTest : IClassFixture<GameApplicationFactory<Program>>, IDisposable
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    
    public AdminControllerTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
        
        // ensure the game database is nuked.
        // so we start fresh everytime.
        using var scope = _factory.Services.CreateScope();
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        gameDatabase.Database.EnsureDeleted();
    }
    
    public void Dispose()
    {
        // ensure the game database is nuked.
        // so we start fresh everytime.
        using var scope = _factory.Services.CreateScope();
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        gameDatabase.Database.EnsureDeleted();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(627)]
    [InlineData(628)]
    public async Task TrackMarkFail(int trackId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();
        
        var response = await _httpClient.PatchAsync(
            "/api/admin/mark-selected-track",
            new StringContent(
                JsonSerializer.Serialize(new TrackMarkingRequestModel
                {
                    TrackId = trackId,
                    Testing = true
                }), Encoding.UTF8, "application/json")
        );
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("ID can only be [1-626]", await response.Content.ReadAsStringAsync());
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(625)]
    [InlineData(626)]
    public async Task TrackMarkPass(int trackId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.PatchAsync(
            "/api/admin/mark-selected-track",
            new StringContent(
                JsonSerializer.Serialize(new TrackMarkingRequestModel
                {
                    TrackId = trackId,
                    Testing = true
                }), Encoding.UTF8, "application/json")
        );
        
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotStrictEqual("[]", content);
    }

    [Fact]
    public async Task PlayerAndScoreAdditionTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        var response = await _httpClient.PostAsync(
            "/api/admin/add-score",
            new StringContent(
                JsonSerializer.Serialize(new ScoreAdditionRequestModel
                {
                    // @hidr0 on Discord, *mostly* real data.
                    DiscordId = 317587311279734784,
                    FriendCode = 8090803305987,
                    Sub1 = 101.0000,
                    DxScore1 = 6969,
                    Sub2 = 101.0000,
                    DxScore2 = 7270,
                    SchoolId = 237
                }), Encoding.UTF8, "application/json")
        );
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var playerByDiscord = gameDatabase.Players
            .Include(p => p.Score)
            .FirstOrDefault(p => p.Id == 317587311279734784);
        
        Assert.NotNull(playerByDiscord);
        Assert.NotNull(playerByDiscord.Score);
        Assert.True(playerByDiscord.Score.PlayerId == playerByDiscord.Id);
        
        var playerByFriendCode = gameDatabase.Players
            .Include(p => p.Score)
            .FirstOrDefault(p => p.FriendCode == 8090803305987);
        
        Assert.NotNull(playerByFriendCode);
        Assert.NotNull(playerByFriendCode.Score);
        Assert.True(playerByDiscord.Score.PlayerId == playerByDiscord.Id);
        
        await gameDatabase.Database.EnsureDeletedAsync();
    }
    
    [Fact]
    public async Task TeamAdditionTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        var response = await _httpClient.PostAsync(
            "/api/admin/add-team",
            new StringContent(
                JsonSerializer.Serialize(new TeamRequestModel
                {
                    Name = "MSOC"
                }), Encoding.UTF8, "application/json")
        );
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var team = gameDatabase.Teams.FirstOrDefault(t => t.Name == "MSOC");
        
        Assert.NotNull(team);
        Assert.Equal("MSOC", team.Name);
        
        await gameDatabase.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task PlayerAndTeamBindingTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        await _httpClient.PostAsync(
            "/api/admin/add-score",
            new StringContent(
                JsonSerializer.Serialize(new ScoreAdditionRequestModel
                {
                    // @hidr0 on Discord, *mostly* real data.
                    DiscordId = 317587311279734784,
                    FriendCode = 8090803305987,
                    Sub1 = 101.0000,
                    DxScore1 = 6969,
                    Sub2 = 101.0000,
                    DxScore2 = 7270,
                    SchoolId = 237
                }), Encoding.UTF8, "application/json")
        );
        
        await _httpClient.PostAsync(
            "/api/admin/add-team",
            new StringContent(
                JsonSerializer.Serialize(new TeamRequestModel
                {
                    Name = "MSOC"
                }), Encoding.UTF8, "application/json")
        );
        
        var response = await _httpClient.PatchAsync(
            "/api/admin/bind-player-to-team",
            new StringContent(
                JsonSerializer.Serialize(new PlayerBindingRequestModel
                {
                    PlayerId = 317587311279734784,
                    TeamId = 1
                }), Encoding.UTF8, "application/json")
        );
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var editedTeam = gameDatabase.Teams
            .Include(t => t.Players)
            .First(t => t.Name == "MSOC");
        
        Assert.NotNull(editedTeam);
        Assert.StrictEqual(1, editedTeam.Players.Count);
        
        await gameDatabase.Database.EnsureDeletedAsync();
    }
}