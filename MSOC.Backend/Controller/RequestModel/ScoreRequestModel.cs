namespace MSOC.Backend.Controller.RequestModel;

public class ScoreRequestModel
{
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
}