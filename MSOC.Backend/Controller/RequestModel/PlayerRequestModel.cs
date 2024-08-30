using System.ComponentModel.DataAnnotations;

namespace MSOC.Backend.Controller.RequestModel;

public class PlayerRequestModel
{
    /// <summary>
    ///     Discord ID associated with this player.
    /// </summary>
    public ulong DiscordId { get; set; }
    
    /// <summary>
    ///     maimai Friend Code associated with this player.
    /// </summary>
    public ulong FriendCode { get; set; }

    /// <summary>
    ///     Whether this player is a leader for their team.
    /// </summary>
    public bool IsLeader { get; set; }
}