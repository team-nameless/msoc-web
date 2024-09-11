using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Middleware;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/leaderboard")]
public class LeaderboardController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;

    public LeaderboardController(GameDatabaseService gameDatabase)
    {
        _gameDatabase = gameDatabase;
    }

    /// <summary>
    ///     Get individual leaderboard. Expect shit performance.
    /// </summary>
    /// <param name="page">Page number, starting from 1</param>
    [HttpGet("individual")]
    [ProducesResponseType(typeof(IEnumerable<Score>), 200, "application/json")]
    public IActionResult QueryIndividualLeaderboard([FromQuery] int page = 1)
    {
        if (page < 1) return BadRequest("Page number must be at least 1");

        var sortedScores = _gameDatabase.Scores
            .AsNoTracking()
            .Where(score => score.IsAccepted)
            .OrderByDescending(score => score.Sub1 + score.Sub2)
            .ThenByDescending(score => score.DxScore1 + score.DxScore2)
            .ThenBy(score => score.DateOfAdmission)
            .Skip(10 * (page - 1))
            .Take(10)
            .Include(s => s.Player)
            .ToList();

        // recursion prevention
        foreach (var score in sortedScores) score.Player.Score = null!;

        return Ok(sortedScores);
    }

    /// <summary>
    ///     Get TOP 8 of team leaderboard. Expect shit performance.
    /// </summary>
    [HttpGet("team")]
    [ProducesResponseType(typeof(IEnumerable<Team>), 200, "application/json")]
    public IActionResult QueryTeamLeaderboard()
    {
        var sortedTeams = _gameDatabase.Teams
            .AsNoTracking()
            .Include(t => t.Players)
            .ThenInclude(p => p.Score)
            .OrderByDescending(team => team.Players.Count)
            .Where(team => team.Players.Count(p => p.Score!.IsAccepted) > 0)
            .OrderByDescending(team => team.Players.Sum(p => p.Score!.Sub1 + p.Score.Sub2))
            .ThenByDescending(team => team.Players.Sum(p => p.Score!.DxScore1 + p.Score.DxScore2))
            .Take(8)
            .ToList();

        // recursion prevention
        foreach (var player in sortedTeams.SelectMany(team => team.Players))
        {
            player.Score!.Player = null!;
            player.Team = null!;
        }

        return Ok(sortedTeams);
    }

    /// <summary>
    ///     Approve an entry on the leaderboard.
    /// </summary>
    /// <param name="approval">Approval object.</param>
    [HttpPatch("approve")]
    [ApiKeyAuthorize]
    public IActionResult ApproveScore(
        [FromBody] ScoreApprovalRequestModel approval
    )
    {
        _gameDatabase.Scores
            .Select(score => new { score.Id, score.IsAccepted, score.DateOfAcceptance })
            .Where(score => score.Id == approval.ScoreId)
            .ExecuteUpdate(
                score =>
                    score
                        .SetProperty(s => s.IsAccepted, true)
                        .SetProperty(s => s.DateOfAcceptance, DateTime.Now)
            );

        // TODO: Hit the SignalR endpoint to yell at the front end.

        return Ok();
    }
}