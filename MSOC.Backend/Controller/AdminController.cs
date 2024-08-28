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
    private readonly DatabaseService _database;
    private readonly TrackDatabaseService _trackDatabase;

    public AdminController(DatabaseService database, TrackDatabaseService trackDatabase)
    {
        _database = database;
        _trackDatabase = trackDatabase;
    }
    
    [HttpPost("admin/add-player")]
    public IActionResult AddPlayer([FromBody] PlayerRequestModel player)
    {
        _database.Players.Add(new Player
        {
            Id = player.Id,
            IsLeader = player.IsLeader,
            Rating = player.Rating,
            Username = player.Username,
        });
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-school")]
    public IActionResult AddSchool([FromBody] SchoolRequestModel school)
    {
        _database.Schools.Add(new School
        {
            Name = school.Name,
            Type = school.Type
        });
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-score/")]
    public IActionResult AddScore([FromBody] ScoreRequestModel score)
    {
        _database.Scores.Add(new Score
        {
            IsAccepted = false,
            PlayerId = score.PlayerId,
            Sub1 = score.Sub1,
            Sub2 = score.Sub2,
            DxScore1 = score.DxScore1,
            DxScore2 = score.DxScore2,
            DateOfAdmission = DateTime.Now,
        });
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpPost("admin/add-team")]
    public IActionResult AddTeam([FromBody] TeamRequestModel team)
    {
        _database.Teams.Add(new Team
        {
            Name = team.Name,
            Players = {  }
        });
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpGet("admin/bind-team")]
    public IActionResult BindTeamToPlayer(int teamId, ulong playerId)
    {
        Team t = _database.Teams.First(t => t.Id == teamId);
        Player p = _database.Players.First(p => p.Id == playerId);
        
        t.Players.Add(p);
        p.Team = t;
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpGet("admin/bind-school")]
    public IActionResult BindSchoolToPlayer(int schoolId, ulong playerId)
    {
        School s = _database.Schools.First(s => s.Id == schoolId);
        Player p = _database.Players.First(p => p.Id == playerId);
        
        p.School = s;
        
        _database.SaveChanges();

        return Ok();
    }
    
    [HttpGet("admin/approve-score")]
    public IActionResult ApproveScore(ulong scoreId)
    {
        var scores = _database.Scores.Where(score => score.Id == scoreId);
        
        var player = _database.Players
            .Include(p => p.Team)
            .Include(p => p.School)
            .Include(p => p.Score)
            .First(p => p.Id == 6969);

        foreach (var score in scores)
        {
            score.IsAccepted = true;
            score.DateOfAcceptance = DateTime.Now;
        }

        // Do an update on the entire database.
        var _ = _database.Scores
            .Where(score => score.IsAccepted)
            .OrderByDescending(score => score.Sub1 + score.Sub2)
            .ThenByDescending(score => score.DxScore1 + score.DxScore2)
            .ThenBy(score => score.DateOfAdmission);

        // TODO: Hit the SignalR endpoint to yell at the front end.

        // TODO: might not need this fr fr
        // _database.SaveChanges();

        return Ok();
    }

    [HttpPost("tracks/add")]
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
}