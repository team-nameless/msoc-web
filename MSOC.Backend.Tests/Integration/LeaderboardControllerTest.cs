using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Integration;

public class LeaderboardControllerTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonCaseInsensitive = new() { PropertyNameCaseInsensitive = true };
    
    public LeaderboardControllerTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
        var configuration = _factory.Services.GetService<IConfiguration>()!;
        
        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            configuration.GetSection("API:Authorization").Value
        );
    }

    [Fact]
    public async Task IndividualLeaderboardTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        for (ulong testId = 1001; testId <= 1005; testId++)
        {
            await _httpClient.PostAsync(
                "/api/admin/add-score",
                new StringContent(
                    JsonSerializer.Serialize(new ScoreAdditionRequestModel
                    {
                        DiscordId = testId,
                        FriendCode = 8090803305987,
                        Sub1 = Random.Shared.Next(97, 101),
                        DxScore1 = 6969,
                        Sub2 = Random.Shared.Next(97, 101),
                        DxScore2 = 7270,
                        SchoolId = 123
                    }), Encoding.UTF8, "application/json")
            );
        }

        for (ulong testId = 1; testId <= 5; testId++)
            await _httpClient.PatchAsync(
                "/api/admin/approve-leaderboard",
                new StringContent(JsonSerializer.Serialize(new ScoreApprovalRequestModel
                {
                    ScoreId = testId
                }), Encoding.UTF8, "application/json")
            );

        var response = await _httpClient.GetAsync("/api/leaderboard/individual?page=1");
        var content = await response.Content.ReadAsStringAsync();
        
        var scores = JsonSerializer.Deserialize<List<Score>>(content, _jsonCaseInsensitive);
        
        Assert.StrictEqual(5, scores!.Count);
        scores.ForEach(score => Assert.Null(score.Player.Score));

        var sortedScores = scores.OrderByDescending(score => score.Sub1 + score.Sub2)
            .ThenByDescending(score => score.DxScore1 + score.DxScore2)
            .ThenBy(score => score.DateOfAdmission);
        
        Assert.Equal(scores, sortedScores);
        
        await gameDatabase.Database.EnsureDeletedAsync();
    }
    
    [Fact]
    public async Task TeamLeaderboardTest()
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        // add test players
        for (ulong testId = 1001; testId <= 1032; testId++)
        {
            await _httpClient.PostAsync(
                "/api/admin/add-score",
                new StringContent(
                    JsonSerializer.Serialize(new ScoreAdditionRequestModel
                    {
                        DiscordId = testId,
                        FriendCode = 8090803305987,
                        Sub1 = Random.Shared.Next(97, 101),
                        DxScore1 = 6969,
                        Sub2 = Random.Shared.Next(97, 101),
                        DxScore2 = 7270,
                        SchoolId = Random.Shared.Next(123, 150)
                    }), Encoding.UTF8, "application/json")
            );
        }

        // add test teams
        for (ulong teamId = 1; teamId <= 8; teamId++)
        {
            await _httpClient.PostAsync(
                "/api/admin/add-team",
                new StringContent(
                    JsonSerializer.Serialize(new TeamRequestModel
                    {
                        Name = $"MSOC-TestTeam-{teamId}",
                    }), Encoding.UTF8, "application/json")
            );
        }
        
        // approve all scores
        for (ulong scoreId = 1; scoreId <= 32; scoreId++)
        {
            await _httpClient.PatchAsync(
                "/api/admin/approve-leaderboard",
                new StringContent(JsonSerializer.Serialize(new ScoreApprovalRequestModel
                {
                    ScoreId = scoreId
                }), Encoding.UTF8, "application/json")
            );
        }
        
        // perform team binding
        for (ulong testId = 1001; testId <= 1032; testId++)
        {
            await _httpClient.PatchAsync(
                "/api/admin/bind-player-to-team",
                new StringContent(
                    JsonSerializer.Serialize(new PlayerBindingRequestModel
                    {
                        PlayerId = testId,
                        TeamId = (int) ((testId - 1001) / 4) + 1
                    }), Encoding.UTF8, "application/json")
            );
        }
        
        var response = await _httpClient.GetAsync("/api/leaderboard/team");
        var content = await response.Content.ReadAsStringAsync();
        
        var teams = JsonSerializer.Deserialize<List<Team>>(content, _jsonCaseInsensitive);
        
        Assert.StrictEqual(8, teams!.Count);
        
        teams.ForEach(team => team.Players.ToList().ForEach(player =>
            {
                Assert.Null(player.Score!.Player);
                Assert.Null(player.Team);
            }
        ));
        
        var sortedTeams = teams
            .OrderByDescending(team => team.Players.Sum(p => p.Score!.Sub1 + p.Score.Sub2))
            .ThenByDescending(team => team.Players.Sum(p => p.Score!.DxScore1 + p.Score.DxScore2));
        
        Assert.Equal(teams, sortedTeams);
        
        await gameDatabase.Database.EnsureDeletedAsync();
    }
}