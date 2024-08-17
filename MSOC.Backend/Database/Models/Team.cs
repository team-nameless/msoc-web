using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a team.
/// </summary>
public class Team
{
    /// <summary>
    ///     Team ID.
    ///     Should be AUTO_INCREMENT I suppose.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Team name.
    /// </summary>
    [MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    ///     Members associated with this team.
    /// </summary>
    [CollectionAccess(CollectionAccessType.Read)]
    public ICollection<Player> Members { get; } = new List<Player>();

    /// <summary>
    ///     Schools associated with this team.
    /// </summary>
    [CollectionAccess(CollectionAccessType.Read)]
    public ICollection<School> Schools { get; } = new List<School>();
}