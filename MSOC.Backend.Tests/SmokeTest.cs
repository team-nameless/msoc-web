using System.Net;

namespace MSOC.Backend.Tests;

public class SmokeTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public SmokeTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }
    
    /// <summary>
    ///     Smoke test(n):
    ///     preliminary testing or sanity testing to reveal simple failures severe enough to,
    ///     for example, reject a prospective software release.
    /// </summary>
    [Fact]
    public async Task ApiSmokeTest()
    {
        var response = await _httpClient.GetAsync("/api/healthcheck");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }
}