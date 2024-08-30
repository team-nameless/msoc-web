using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("select")]
    public IActionResult SelectTracks(
        [FromQuery(Name = "min_diff")] double minDiff = 1.0,
        [FromQuery(Name = "max_diff")] double maxDiff = 15.0
    )
    {
        if (minDiff > maxDiff) return BadRequest("min_diff must be less than or equal to max_diff");
        if (minDiff < 1 || maxDiff > 15) return BadRequest("[min_diff; max_diff] must be in range of [1.0; 15.0]");
        
        var foundedTracks = _trackDatabase.Tracks
            .Where(track => !track.HasBeenPicked)
            .Where(track => minDiff <= track.Constant && track.Constant <= maxDiff)
            .ToArray();
        
        if (foundedTracks.Length == 0) return NotFound();

        Random.Shared.Shuffle(foundedTracks);

        return Ok(foundedTracks.Take(10));
    }

    [HttpGet("mark")]
    public IActionResult MarkTrackAsPicked(
        [FromQuery(Name = "track_id")] int trackId,
        [FromQuery] bool testing = false
    )
    {
        if (trackId is < 1 or > 626) return BadRequest("ID can only be [1-626]");
        
        var foundedTracks = _trackDatabase.Tracks
            .Where(track => track.Id == trackId)
            .Take(1)
            .ToArray();
        
        if (foundedTracks.Length == 0) return NotFound();

        if (!testing)
            foreach (var track in foundedTracks)
            {
                track.HasBeenPicked = true;
                _trackDatabase.Update(track);
                _trackDatabase.SaveChanges();
            }

        return Ok();
    }

    [HttpGet("get")]
    public IActionResult GetTrack(
        [FromQuery(Name = "track_id")] int trackId
    )
    {
        if (trackId is < 1 or > 626) return BadRequest("ID can only be [1-626]");
        
        var foundedTracks = _trackDatabase.Tracks
            .Where(track => track.Id == trackId)
            .Take(1)
            .ToArray();
        
        if (foundedTracks.Length == 0) return NotFound();

        return Ok(foundedTracks[0]);
    }
}