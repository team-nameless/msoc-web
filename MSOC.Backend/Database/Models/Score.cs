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
    public required Player Player { get; set; }

    /// <summary>
    ///     Achievement% score of TRACK 1
    /// </summary>
    public float Sub1 { get; set; }

    /// <summary>
    ///     Achievement% score of TRACK 2
    /// </summary>
    public float Sub2 { get; set; }

    /// <summary>
    ///     Get the time when this submission was accepted.
    /// </summary>
    public DateTime DateOfAdmission { get; set; }
}