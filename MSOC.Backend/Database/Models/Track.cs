using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Enum;

namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a track in maimai.
/// </summary>
[Index(nameof(Title), nameof(Difficulty), nameof(Type))]
public class Track
{
    /// <summary>
    ///     Track ID, be AUTO_INCREMENT or not.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Track TITLE.
    /// </summary>
    [MaxLength(255)]
    public required string Title { get; set; }

    /// <summary>
    ///     Track CATEGORY.
    /// </summary>
    [MaxLength(255)]
    public required string Category { get; set; }

    /// <summary>
    ///     Track ARTIST.
    /// </summary>
    [MaxLength(255)]
    public required string Artist { get; set; }

    /// <summary>
    ///     maimai version where this track appeared.
    /// </summary>
    [MaxLength(255)]
    public required string Version { get; set; }

    /// <summary>
    ///     Track difficulty type.
    /// </summary>
    public TrackDifficulty Difficulty { get; set; }

    /// <summary>
    ///     Track type.
    /// </summary>
    public TrackType Type { get; set; }

    /// <summary>
    ///     Cover image URL.
    /// </summary>
    [MaxLength(255)]
    public required string CoverImageUrl { get; set; }

    /// <summary>
    ///     Chart constant.
    /// </summary>
    public double Constant { get; set; }

    /// <summary>
    ///     If this track has been picked at the tournament.
    /// </summary>
    public bool HasBeenPicked { get; set; }
}