using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Middleware;

public class AdminControllerMiddlewareTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    
    public AdminControllerMiddlewareTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }
    
    [Fact]
    public async Task AdminControllerMiddlewareUnauthorized()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            "Hey I am an administrator!"
        );
        
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
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AdminControllerMiddlewarePass()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var gameDatabase = scope.ServiceProvider.GetService<GameDatabaseService>()!;
        await gameDatabase.Database.EnsureCreatedAsync();
        
        var configuration = scope.ServiceProvider.GetService<IConfiguration>()!;
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            configuration.GetSection("API:Authorization").Value
        );
        
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
    }
}