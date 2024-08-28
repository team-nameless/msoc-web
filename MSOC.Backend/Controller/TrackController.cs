using Microsoft.AspNetCore.Mvc;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[Route("api/tracks")]
[ApiController]
public class TrackController : ControllerBase
{
    private readonly TrackDatabaseService _trackDatabase;

    public TrackController(TrackDatabaseService trackDatabase)
    {
        _trackDatabase = trackDatabase;
    }

    [HttpPost("add")]
    public IActionResult AddTrack([FromBody] Track track)
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

    [HttpGet("select")]
    public IActionResult SelectTracks(
        [FromQuery(Name = "min_diff")] double minDiff = 1.0,
        [FromQuery(Name = "max_diff")] double maxDiff = 15.0
    )
    {
        if (minDiff > maxDiff) return BadRequest("oi!");

        var foundedTracks = _trackDatabase.Tracks
            .Where(track => !track.HasBeenPicked)
            .Where(track => minDiff <= track.Constant && track.Constant <= maxDiff)
            .ToArray();

        Random.Shared.Shuffle(foundedTracks);

        return Ok(foundedTracks.Take(10));
    }

    [HttpGet("mark")]
    public IActionResult MarkTrackAsPicked([FromQuery] int trackId)
    {
        var foundedTracks = _trackDatabase.Tracks
            .Where(track => track.Id == trackId)
            .Take(1);

        foreach (var track in foundedTracks)
        {
            track.HasBeenPicked = true;
            _trackDatabase.Update(track);
            _trackDatabase.SaveChanges();
        }

        return Ok();
    }
}