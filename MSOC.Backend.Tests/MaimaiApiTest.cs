using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MSOC.Backend.Tests;

public class MaimaiApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _programFactory;

    public MaimaiApiTest(WebApplicationFactory<Program> programFactory)
    {
        _programFactory = programFactory;
    }
    
    [Theory]
    [InlineData(1234)]
    [InlineData(69420)]
    [InlineData(177013)]
    public async Task InvalidFriendCodeTest(ulong friendCode)
    {
        var httpClient = _programFactory.CreateDefaultClient();

        var response = await httpClient.PostAsJsonAsync(
            "/api/player/query",
            friendCode
        );
        
        Assert.StrictEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Unable to retrieve needed information.", await response.Content.ReadAsStringAsync());
    }
    
    [Theory]
    [InlineData(9051555929120)]
    [InlineData(8095773611588)]
    [InlineData(9020119099087)]
    public async Task ValidFamiliarFriendCodeTest(ulong friendCode)
    {
        var httpClient = _programFactory.CreateDefaultClient();

        var response = await httpClient.PostAsJsonAsync(
            "/api/player/query",
            friendCode
        );
        
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Theory]
    [InlineData(8069933165057)]
    public async Task ValidStrangerFriendCodeTest(ulong friendCode)
    {
        var httpClient = _programFactory.CreateDefaultClient();

        var response = await httpClient.PostAsJsonAsync(
            "/api/player/query",
            friendCode
        );
        
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
    }
}