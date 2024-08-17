using System.ComponentModel.DataAnnotations;

namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a school.
/// </summary>
public class School
{
    /// <summary>
    ///     School ID.
    ///     Should be AUTO_INCREMENT I suppose.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     School name.
    /// </summary>
    [MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    ///     School type (3=High school, 4=College/University, whatever your english).
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    ///     The team which this school belongs to, mostly for relationship, don't care about it.
    /// </summary>
    public required Team Team { get; set; }
}