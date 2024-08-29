using System.ComponentModel.DataAnnotations;

namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a player.
/// </summary>
public class Player
{
    /// <summary>
    ///     maimai Friend code associated with this user.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    ///     maimai username.
    /// </summary>
    [MaxLength(16)]
    public required string Username { get; set; }

    /// <summary>
    ///     maimai rating.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    ///     Whether this player is a leader for their team.
    /// </summary>
    public bool IsLeader { get; set; }
    
    /// <summary>
    ///     The school this player belongs to.
    /// </summary>
    public int SchoolId { get; set; }

    /// <summary>
    ///     The team this player belongs to.
    /// </summary>
    public Team? Team { get; set; }

    /// <summary>
    ///     The score associated with this player.
    /// </summary>
    public Score? Score { get; set; }
}