using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Middleware;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[Route("api/teams")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;

    public TeamController(GameDatabaseService gameDatabase)
    {
        _gameDatabase = gameDatabase;
    }

    /// <summary>
    ///     Add a team to database.
    /// </summary>
    /// <param name="team">Team object.</param>
    [HttpPost("add")]
    [ApiKeyAuthorize]
    public IActionResult AddTeam([FromBody] TeamRequestModel team)
    {
        _gameDatabase.Teams.Add(new Team
        {
            Name = team.Name
        });

        _gameDatabase.SaveChanges();

        return Ok();
    }

    /// <summary>
    ///     Bind a player to a team.
    /// </summary>
    /// <param name="bind">Bind object.</param>
    [HttpPatch("bind-player")]
    [ApiKeyAuthorize]
    public IActionResult BindPlayerToTeam(
        [FromBody] PlayerBindingRequestModel bind
    )
    {
        var player = _gameDatabase.Players.FirstOrDefault(p => p.Id == bind.PlayerId);
        var team = _gameDatabase.Teams.FirstOrDefault(t => t.Id == bind.TeamId);

        if (player is null || team is null) return NotFound();

        if (team.Players.Count == 4) return BadRequest("Team is already full.");

        team.Players.Add(player);
        _gameDatabase.SaveChanges();

        return Ok();
    }
}