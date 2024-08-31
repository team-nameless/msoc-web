using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Database.Models;
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

        // Do an update on the entire database.
        var sortedScores = _gameDatabase.Scores
            .Include(s => s.Player)
            .Where(score => score.IsAccepted)
            .OrderByDescending(score => score.Sub1 + score.Sub2)
            .ThenByDescending(score => score.DxScore1 + score.DxScore2)
            .ThenBy(score => score.DateOfAdmission)
            .Skip(10 * (page - 1))
            .Take(10);

        // recursion prevention
        foreach (var score in sortedScores) score.Player.Score = null!;
        
        // TODO: Hit the SignalR endpoint to yell at the front end.
        // _database.SaveChanges();

        return Ok(sortedScores);
    }

    /// <summary>
    ///     Get team leaderboard. Expect shit performance.
    /// </summary>
    [HttpGet("team")]
    [ProducesResponseType(typeof(IEnumerable<Team>), 200, "application/json")]
    public IActionResult QueryTeamLeaderboard()
    {
        // Do an update on the entire database.
        var sortedScores = _gameDatabase.Teams
            .Include(t => t.Players)
            .OrderByDescending(team => team.Players.Sum(p => p.Score!.Sub1 + p.Score.Sub2))
            .ThenByDescending(team => team.Players.Sum(p => p.Score!.DxScore1 + p.Score.DxScore2))
            .Take(8);

        // TODO: Hit the SignalR endpoint to yell at the front end.
        // _database.SaveChanges();

        return Ok(sortedScores);
    }
}