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

public class PlayerControllerTest : IClassFixture<GameApplicationFactory<Program>>, IDisposable
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonCaseInsensitive = new() { PropertyNameCaseInsensitive = true };

    public PlayerControllerTest(GameApplicationFactory<Program> factory)
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

    [Theory]
    [InlineData(317587311279734784, "discord")]
    [InlineData(8090803305987, "friend_code")]
    public async Task PlayerQueryTest(ulong key, string type)
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

        var response = await _httpClient.GetAsync($"/api/player/score?id={key}&type={type}");
        var content = await response.Content.ReadAsStringAsync();
        System.Console.WriteLine(content);

        var data = JsonSerializer.Deserialize<Player>(content, _jsonCaseInsensitive);

        Assert.NotNull(data);
        Assert.NotNull(data.Score);
        Assert.Null(data.Score.Player);

        await gameDatabase.Database.EnsureDeletedAsync();
    }
}