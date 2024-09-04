namespace MSOC.Backend.Controller.RequestModel;

public class ScoreAdditionRequestModel
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
    ///     Achievement% score of TRACK 1.
    /// </summary>
    public double Sub1 { get; set; }

    /// <summary>
    ///     DX score of TRACK 1.
    /// </summary>
    public int DxScore1 { get; set; }

    /// <summary>
    ///     Achievement% score of TRACK 2
    /// </summary>
    public double Sub2 { get; set; }

    /// <summary>
    ///     DX score of TRACK 2.
    /// </summary>
    public int DxScore2 { get; set; }

    /// <summary>
    ///     School ID.
    /// </summary>
    public int SchoolId { get; set; }
}