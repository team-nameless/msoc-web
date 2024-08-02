using Microsoft.AspNetCore.Mvc;
using AngleSharp.Html.Parser;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/player")]
public class PlayerController(IConfiguration configuration) : ControllerBase
{
    private HttpClient _httpClient = new ();
    private IConfiguration _configuration = configuration;

    [HttpPost("query")]
    public async Task<IActionResult> QueryUserByFriendCode([FromForm] ulong? friendCode)
    {
        if (friendCode == null)
        {
            return BadRequest();
        }
        
        await AuthenticateWithSegaServer();
        
        var result = await _httpClient
            .GetAsync($"https://maimaidx-eng.com/maimai-mobile/friend/search/searchUser/?friendCode={friendCode}");

        if (result.RequestMessage?.RequestUri?.AbsoluteUri == "https://maimaidx-eng.com/maimai-mobile/error/")
        {
            return StatusCode(502);
        }
        
        var html = await result.Content.ReadAsStringAsync();
        var parser = new HtmlParser();
        var document = parser.ParseDocument(html).All;

        var needed = document.Where(x =>
            x is { LocalName: "div", ClassName: "name_block f_l f_16" or "rating_block" }).ToArray();

        if (needed.Length == 0)
        {
            return BadRequest();
        }
        
        var name = needed[0].TextContent;
        var rating = needed[1].TextContent;

        return new JsonResult(new Dictionary<string, string>
        {
            ["name"] = name,
            ["rating"] = rating
        });
    }
    
    private async Task AuthenticateWithSegaServer()
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

        // Auth page 1st hit for cookie population
        await _httpClient.GetAsync(
            "https://lng-tgk-aime-gw.am-all.net/common_auth/login?site_id=maimaidxex&redirect_url=https://maimaidx-eng.com/maimai-mobile/&back_url=https://maimai.sega.com/");

        // The actual auth
        await _httpClient.PostAsync(
            "https://lng-tgk-aime-gw.am-all.net/common_auth/login/sid",
            new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("sid", _configuration.GetValue<string>("SEGA:USERNAME") ?? ""),
                new("password", _configuration.GetValue<string>("SEGA:PASSWORD") ?? ""),
                new("retention", "1")
            }));
    }
}