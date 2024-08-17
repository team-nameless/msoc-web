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
    public string Username { get; set; }

    /// <summary>
    ///     maimai rating.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    ///     From which school is this player from.
    ///     The reason why this is NOT the concrete type <see cref="School" />
    ///     because it would fuck around with the relationship.
    /// </summary>
    public int SchoolId { get; set; }

    /// <summary>
    ///     Whether this player is a leader for their team.
    /// </summary>
    public bool IsLeader { get; set; }

    /// <summary>
    ///     The team this player belongs to.
    /// </summary>
    public required Team Team { get; set; }
}