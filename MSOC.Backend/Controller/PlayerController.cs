using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Middleware;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/player")]
public class PlayerController : ControllerBase
{
    private readonly MaimaiInquiryService _maimai;
    private readonly GameDatabaseService _gameDatabase;

    public PlayerController(MaimaiInquiryService maimai, GameDatabaseService gameDatabase)
    {
        _maimai = maimai;
        _gameDatabase = gameDatabase;
    }

    /// <summary>
    ///     Get player data.
    /// </summary>
    /// <param name="lookupKey">Key index to look up.</param>
    /// <param name="queryType">Type of the key - either "discord" or "friend_code".</param>
    [HttpGet("score")]
    [ProducesResponseType(typeof(Player), 200, "application/json")]
    public IActionResult GetPlayer(
        [FromQuery(Name = "id")] ulong lookupKey,
        [FromQuery(Name = "type")] string queryType = "friend_code"
    )
    {
        var defaultQuery = _gameDatabase.Players
            .AsNoTracking()
            .Include(p => p.Score)
            .Include(p => p.Team);

        var player = queryType switch
        {
            "friend_code" => defaultQuery.FirstOrDefault(p => p.FriendCode == lookupKey),
            "discord" => defaultQuery.FirstOrDefault(p => p.Id == lookupKey),
            _ => null
        };

        if (player == null) return NotFound();

        // we prevent recursion?
        player.Score!.Player = null!;

        return Ok(player);
    }

    /// <summary>
    ///     Add score to database.
    /// </summary>
    /// <param name="score">Score object.</param>
    [HttpPost("score")]
    [ApiKeyAuthorize]
    public async Task<IActionResult> AddScore([FromBody] ScoreAdditionRequestModel score)
    {
        var maiInfo = await _maimai.PerformFriendCodeLookupAsync(score.FriendCode);

        _gameDatabase.Players.Add(new Player
        {
            Id = score.DiscordId,
            FriendCode = score.FriendCode,
            IsLeader = false,
            Username = maiInfo[0].TextContent,
            Rating = Convert.ToInt32(maiInfo[1].TextContent),
            MaimaiAvatarUrl = (maiInfo[2] as IHtmlImageElement)!.Source!,
            SchoolId = score.SchoolId
        });

        _gameDatabase.Scores.Add(new Score
        {
            IsAccepted = false,
            PlayerId = score.DiscordId,
            Sub1 = score.Sub1,
            Sub2 = score.Sub2,
            DxScore1 = score.DxScore1,
            DxScore2 = score.DxScore2,
            DateOfAdmission = DateTime.Now
        });

        await _gameDatabase.SaveChangesAsync();

        return Ok();
    }
}