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
    ///     idk
    /// </summary>
    public int SchoolId { get; set; }

    /// <summary>
    ///     Whether this player is a leader for their team.
    /// </summary>
    public bool IsLeader { get; set; }

    /// <summary>
    ///     Achievement% score of TRACK 1
    /// </summary>
    public float Sub1 { get; set; }

    /// <summary>
    ///     Achievement% score of TRACK 2
    /// </summary>
    public float Sub2 { get; set; }

    /// <summary>
    ///     The team this player belongs to.
    /// </summary>
    public Team Team { get; set; } = null!;
    
    /// <summary>
    ///     Get the time when this player's submission was accepted. 
    /// </summary>
    public DateTime DateOfAdmission { get; set; }
}