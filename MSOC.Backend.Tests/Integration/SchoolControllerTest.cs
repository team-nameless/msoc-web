using System.Net;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Integration;

public class SchoolControllerTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public SchoolControllerTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(257)]
    [InlineData(258)]
    public async Task SchoolGetFail(int schoolId)
    {
        await using var scope = _factory.Services.CreateAsyncScope();

        var schoolDatabase = scope.ServiceProvider.GetService<SchoolDatabaseService>()!;
        await schoolDatabase.Database.EnsureCreatedAsync();
        
        var response = await _httpClient.GetAsync($"/api/schools/get?school_id={schoolId}");
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("ID can only be [1-256]", content);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(255)]
    [InlineData(256)]
    public async Task SchoolGetPass(int schoolId)
    { 
        await using var scope = _factory.Services.CreateAsyncScope();
        
        var trackDatabase = scope.ServiceProvider.GetService<SchoolDatabaseService>()!;
        await trackDatabase.Database.EnsureCreatedAsync();

        var response = await _httpClient.GetAsync($"/api/schools/get?school_id={schoolId}");
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(content);
    }
}