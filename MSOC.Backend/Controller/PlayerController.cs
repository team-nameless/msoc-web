using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/player")]
public class PlayerController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;

    public PlayerController(GameDatabaseService gameDatabase)
    {
        _gameDatabase = gameDatabase;
    }

    [HttpGet("get")]
    public IActionResult GetPlayer(
        [FromQuery(Name = "id")] ulong lookupKey,
        [FromQuery(Name = "type")] string queryType = "friend_code"
    )
    {
        var defaultQuery = _gameDatabase.Players
            .Include(p => p.Score)
            .Include(p => p.Team);
        
        var player = queryType switch
        {
            "friend_code" => defaultQuery.FirstOrDefault(p => p.FriendCode == lookupKey),
            "discord" => defaultQuery.FirstOrDefault(p => p.Id == lookupKey),
            _ => null
        };

        if (player == null) return NotFound();

        return Ok(player);
    }
}