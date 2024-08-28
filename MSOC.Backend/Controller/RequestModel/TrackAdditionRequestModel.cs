using MSOC.Backend.Enum;

namespace MSOC.Backend.Controller.RequestModel;

public class TrackAdditionRequestModel
{
    public string Title { get; set; }
    
    public string Artist { get; set; }

    public string Category { get; set; }
    
    public double Constant { get; set; }
    
    public string CoverImageUrl { get; set; }

    public TrackDifficulty Difficulty { get; set; }
    
    public string Version { get; set; }
    
    public TrackType Type { get; set; }
}