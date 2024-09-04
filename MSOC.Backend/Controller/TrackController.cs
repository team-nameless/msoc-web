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

    /// <summary>
    ///     Selects 10 random tracks from given difficulty range.
    /// </summary>
    /// <param name="minDiff">Minimum difficulty, at least 1.0</param>
    /// <param name="maxDiff">Maximum difficulty, at most 15.0</param>
    [HttpGet("select")]
    [ProducesResponseType(typeof(IEnumerable<Track>), 200, "application/json")]
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

    /// <summary>
    ///     Get a track from given ID.
    /// </summary>
    /// <param name="trackId">Track ID, within range 1-626.</param>
    [HttpGet("get")]
    [ProducesResponseType(typeof(Track), 200, "application/json")]
    public IActionResult GetTrack(
        [FromQuery(Name = "track_id")] int trackId
    )
    {
        if (trackId is < 1 or > 626) return BadRequest("ID can only be [1-626]");

        var foundedTrack = _trackDatabase.Tracks
            .FirstOrDefault(track => track.Id == trackId);

        if (foundedTrack == null) return NotFound();

        return Ok(foundedTrack);
    }
}