using System.ComponentModel.DataAnnotations;

namespace MSOC.Backend.Controller.RequestModel;

public class PlayerRequestModel
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
}