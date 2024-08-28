namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a scoreboard.
/// </summary>
public class Score
{
    /// <summary>
    ///     Scoreboard ID.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    ///     The player associated with this scoreboard entry.
    /// </summary>
    public Player Player { get; set; } = null!;
    
    /// <summary>
    ///     The player ID associated with the scoreboard entry.
    /// </summary>
    public ulong PlayerId { get; set; }

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
    ///     Get the time when this submission was sent.
    /// </summary>
    public DateTime DateOfAdmission { get; set; }
    
    /// <summary>
    ///     Get the time when this submission was accepted.
    /// </summary>
    public DateTime DateOfAcceptance { get; set; }
    
    /// <summary>
    ///     Whether the score is screened.
    /// </summary>
    public bool IsAccepted { get; set; }
}