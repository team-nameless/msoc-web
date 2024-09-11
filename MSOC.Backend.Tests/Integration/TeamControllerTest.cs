using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Integration;

public class TeamControllerTest : IClassFixture<GameApplicationFactory<Program>>, IDisposable
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonCaseInsensitive = new() { PropertyNameCaseInsensitive = true };

    public TeamControllerTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;

        var configuration = _factory.Services.GetService<IConfiguration>()!;

        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            configuration.GetSection("API:Authorization").Value
        );

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

    [Fact]
    public async Task TeamQueryTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.PostAsync(
            "/api/teams/add",
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
            "/api/player/score",
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
            "/api/teams/add",
            new StringContent(
                JsonSerializer.Serialize(new TeamRequestModel
                {
                    Name = "MSOC"
                }), Encoding.UTF8, "application/json")
        );

        var response = await _httpClient.PatchAsync(
            "/api/teams/bind-player",
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