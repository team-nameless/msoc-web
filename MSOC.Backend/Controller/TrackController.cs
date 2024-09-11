﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Middleware;
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
            .AsNoTracking()
            .Where(track => minDiff <= track.Constant && track.Constant <= maxDiff)
            .Where(track => !track.HasBeenPicked)
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
            .AsNoTracking()
            .FirstOrDefault(track => track.Id == trackId);

        if (foundedTrack == null) return NotFound();

        return Ok(foundedTrack);
    }

    /// <summary>
    ///     Add a track to database.
    /// </summary>
    /// <param name="track">Track object.</param>
    [HttpPost("add")]
    [ApiKeyAuthorize]
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
    ///     Marks a track as picked.
    /// </summary>
    /// <param name="trackMark">Track marking object.</param>
    [HttpPatch("mark-selected")]
    [ApiKeyAuthorize]
    public IActionResult MarkTrackAsPicked(
        [FromBody] TrackMarkingRequestModel trackMark
    )
    {
        if (trackMark.TrackId is < 1 or > 626) return BadRequest("ID can only be [1-626]");

        _trackDatabase.Tracks
            .Select(track => new { track.Id, track.HasBeenPicked })
            .Where(track => track.Id == trackMark.TrackId)
            .ExecuteUpdate(
                track =>
                    track
                        .SetProperty(t => t.HasBeenPicked, !trackMark.Testing)
            );

        return Ok();
    }
}