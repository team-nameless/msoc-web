using System.ComponentModel.DataAnnotations;
using MSOC.Backend.Enum;

namespace MSOC.Backend.Controller.RequestModel;

public class SchoolRequestModel
{
    [MaxLength(255)] public required string Name { get; set; }

    public SchoolType Type { get; set; }
}