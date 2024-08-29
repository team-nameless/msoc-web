using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api")]
public class AdminController : ControllerBase
{
    private readonly GameDatabaseService _gameDatabase;
    private readonly TrackDatabaseService _trackDatabase;
    private readonly SchoolDatabaseService _schoolDatabase;
    
    public AdminController(GameDatabaseService gameDatabase, TrackDatabaseService trackDatabase, SchoolDatabaseService schoolDatabase)
    {
        _gameDatabase = gameDatabase;
        _trackDatabase = trackDatabase;
        _schoolDatabase = schoolDatabase;
    }
    
    [HttpPost("admin/add-player")]
    public IActionResult AddPlayer([FromBody] PlayerRequestModel player)
    {
        _gameDatabase.Players.Add(new Player
        {
            Id = player.Id,
            IsLeader = player.IsLeader,
            Rating = player.Rating,
            Username = player.Username,
        });
        
        _gameDatabase.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-school")]
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
    
    [HttpPost("admin/add-score/")]
    public IActionResult AddScore([FromBody] ScoreRequestModel score)
    {
        _gameDatabase.Scores.Add(new Score
        {
            IsAccepted = false,
            PlayerId = score.PlayerId,
            Sub1 = score.Sub1,
            Sub2 = score.Sub2,
            DxScore1 = score.DxScore1,
            DxScore2 = score.DxScore2,
            DateOfAdmission = DateTime.Now,
        });
        
        _gameDatabase.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-team")]
    public IActionResult AddTeam([FromBody] TeamRequestModel team)
    {
        _gameDatabase.Teams.Add(new Team
        {
            Name = team.Name
        });
        
        _gameDatabase.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-track")]
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
    
    [HttpGet("admin/bind-team")]
    public IActionResult BindTeamToPlayer(int teamId, ulong playerId)
    {
        Team t = _gameDatabase.Teams.First(t => t.Id == teamId);
        Player p = _gameDatabase.Players.First(p => p.Id == playerId);
        
        t.Players.Add(p);
        p.Team = t;
        
        _gameDatabase.SaveChanges();

        return Ok();
    }
    
    [HttpGet("admin/bind-school")]
    public IActionResult BindSchoolToPlayer(int schoolId, ulong playerId)
    {
        School s = _schoolDatabase.Schools.First(s => s.Id == schoolId);
        Player p = _gameDatabase.Players.First(p => p.Id == playerId);
        
        p.SchoolId = s.Id;
        
        _gameDatabase.SaveChanges();

        return Ok();
    }
    
    [HttpGet("leaderboard/approve")]
    public IActionResult ApproveScore(ulong scoreId)
    {
        var scores = _gameDatabase.Scores.Where(score => score.Id == scoreId);
        
        var player = _gameDatabase.Players
            .Include(p => p.Team)
            .Include(p => p.SchoolId)
            .Include(p => p.Score)
            .First(p => p.Id == 6969);

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
}