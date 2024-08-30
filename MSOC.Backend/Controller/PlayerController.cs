using Microsoft.AspNetCore.Mvc;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/player")]
public class PlayerController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;
    private readonly MaimaiInquiryService _maimai;

    public PlayerController(MaimaiInquiryService maimai, GameDatabaseService gameDatabase)
    {
        _maimai = maimai;
        _gameDatabase = gameDatabase;
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryUserByDiscord(
        [FromQuery(Name = "id")] ulong discordId
    )
    {
        var player = _gameDatabase.Players.FirstOrDefault(player => player.Id == discordId);
        if (player == null) return NotFound();
        
        var needed = await _maimai.PerformFriendCodeLookupAsync(player.FriendCode);

        if (needed.Length == 0) return BadRequest("Unable to retrieve needed information.");

        var name = needed[0].TextContent;
        var rating = needed[1].TextContent; 

        return new JsonResult(new Dictionary<string, string>
        {
            ["name"] = name,
            ["rating"] = rating
        });
    }
}