using System.Diagnostics;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;
    private readonly MaimaiInquiryService _maimai;
    private readonly SchoolDatabaseService _schoolDatabase;
    private readonly TrackDatabaseService _trackDatabase;

    public AdminController(
        GameDatabaseService gameDatabase,
        TrackDatabaseService trackDatabase,
        SchoolDatabaseService schoolDatabase,
        MaimaiInquiryService maimai
    )
    {
        _gameDatabase = gameDatabase;
        _trackDatabase = trackDatabase;
        _schoolDatabase = schoolDatabase;
        _maimai = maimai;
    }

    /// <summary>
    ///     Add score to database.
    /// </summary>
    /// <param name="score">Score object.</param>
    [HttpPost("add-score")]
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

    /// <summary>
    ///     Add a school to database.
    /// </summary>
    /// <param name="school">School object.</param>
    [HttpPost("add-school")]
    public IActionResult AddSchool([FromBody] SchoolRequestModel school)
    {
        _schoolDatabase.Schools.Add(new School
        {
            Name = school.Name,
            Type = school.Type
        });

        _schoolDatabase.SaveChanges();

        return Ok();
    }

    /// <summary>
    ///     Add a team to database.
    /// </summary>
    /// <param name="team">Team object.</param>
    [HttpPost("add-team")]
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
    ///     Add a track to database.
    /// </summary>
    /// <param name="track">Track object.</param>
    [HttpPost("add-track")]
    public IActionResult AddTrack([FromBody] TrackAdditionRequestModel track)
    {
        _trackDatabase.Tracks.Add(new Track
        {
            Title = track.Title,
            Artist = track.Artist,
            Category = track.Category,
            Constant = track.Constant,
            CoverImageUrl = track.CoverImageUrl,
            Difficulty = track.Difficulty,
            Version = track.Version,
            Type = track.Type,
            HasBeenPicked = false
        });

        _trackDatabase.SaveChanges();

        return Ok();
    }

    /// <summary>
    ///     Bind a player to a team.
    /// </summary>
    /// <param name="bind">Bind object.</param>
    [HttpPatch("bind-player-to-team")]
    public IActionResult BindPlayerToTeam(
        [FromBody] PlayerBindingRequestModel bind
    )
    {
        var player = _gameDatabase.Players.FirstOrDefault(p => p.Id == bind.PlayerId);
        var team = _gameDatabase.Teams.FirstOrDefault(t => t.Id == bind.TeamId);

        if (player is null || team is null) return NotFound();
        team.Players.Add(player);
        _gameDatabase.SaveChanges();
        
        return Ok();
    }
    
    /// <summary>
    ///     Approve an entry on the leaderboard.
    /// </summary>
    /// <param name="approval">Approval object.</param>
    [HttpPatch("approve-leaderboard")]
    public IActionResult ApproveScore(
        [FromBody] ScoreApprovalRequestModel approval
    )
    {
        var scores = _gameDatabase.Scores.Where(score => score.Id == approval.ScoreId).ToArray();

        if (scores.Length == 0) return NotFound();
        
        foreach (var score in scores)
        {
            score.IsAccepted = true;
            score.DateOfAcceptance = DateTime.Now;
        }
        
        // Do an update on the entire database.
        var _ = _gameDatabase.Scores
            .Where(score => score.IsAccepted)
            .OrderByDescending(score => score.Sub1 + score.Sub2)
            .ThenByDescending(score => score.DxScore1 + score.DxScore2)
            .ThenBy(score => score.DateOfAdmission);

        // TODO: Hit the SignalR endpoint to yell at the front end.

        _gameDatabase.SaveChanges();
        
        return Ok();
    }

    /// <summary>
    ///     Marks a track as picked.
    /// </summary>
    /// <param name="trackMark">Track marking object.</param>
    [HttpPatch("mark-selected-track")]
    public IActionResult MarkTrackAsPicked(
        [FromBody] TrackMarkingRequestModel trackMark
    )
    {
        if (trackMark.TrackId is < 1 or > 626) return BadRequest("ID can only be [1-626]");

        var foundedTracks = _trackDatabase.Tracks
            .Where(track => track.Id == trackMark.TrackId)
            .Take(1)
            .ToArray();

        if (foundedTracks.Length == 0) return NotFound();

        if (!trackMark.Testing)
            foreach (var track in foundedTracks)
            {
                track.HasBeenPicked = true;
                _trackDatabase.Update(track);
                _trackDatabase.SaveChanges();
            }

        return Ok();
    }
}