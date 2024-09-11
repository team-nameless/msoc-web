using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Integration;

public class TrackControllerTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public TrackControllerTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;

        var configuration = _factory.Services.GetService<IConfiguration>()!;

        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            configuration.GetSection("API:Authorization").Value
        );
    }

    [Theory]
    [InlineData(0, 15)]
    [InlineData(1, 15.1)]
    [InlineData(0, 15.1)]
    public async Task TrackSelectInvalidBoundariesFail(double minDiff, double maxDiff)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/tracks/select?min_diff={minDiff}&max_diff={maxDiff}");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("[min_diff; max_diff] must be in range of [1.0; 15.0]", content);
    }

    [Theory]
    [InlineData(15.0, 1.0)]
    [InlineData(14.0, 2.0)]
    [InlineData(13.0, 3.0)]
    [InlineData(12.0, 4.0)]
    [InlineData(11.0, 5.0)]
    public async Task TrackSelectReversedBoundariesFail(double minDiff, double maxDiff)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/tracks/select?min_diff={minDiff}&max_diff={maxDiff}");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("min_diff must be less than or equal to max_diff", content);
    }

    [Theory]
    [InlineData(1.0, 11.0)]
    [InlineData(2.0, 12.0)]
    [InlineData(3.0, 13.0)]
    [InlineData(4.0, 14.0)]
    [InlineData(5.0, 15.0)]
    public async Task TrackSelectPass(double minDiff, double maxDiff)
    {
        using var scope = _factory.Services.CreateScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/tracks/select?min_diff={minDiff}&max_diff={maxDiff}");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotStrictEqual("[]", content);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(627)]
    [InlineData(628)]
    public async Task TrackGetFail(int trackId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/tracks/get?track_id={trackId}");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("ID can only be [1-626]", content);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(625)]
    [InlineData(626)]
    public async Task TrackGetPass(int trackId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/tracks/get?track_id={trackId}");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(content);
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
            "/api/tracks/mark-selected",
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
    [InlineData(625)]
    [InlineData(626)]
    public async Task TrackMarkPass(int trackId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var trackDatabase = scope.ServiceProvider.GetService<TrackDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.PatchAsync(
            "/api/tracks/mark-selected",
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
}