using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Enum;

namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a school.
/// </summary>
[Index(nameof(Name), nameof(Type), IsUnique = true)]
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
    ///     School type.
    /// </summary>
    public SchoolType Type { get; set; }
}