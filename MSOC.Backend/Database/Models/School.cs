namespace MSOC.Backend.Database.Models;

/// <summary>
///     Representing a school.
/// </summary>
public class School
{
    /// <summary>
    /// School Id because we need a primary key.
    /// </summary>
    public int Id { get; set; } // AUTO_INCREMENT

    /// <summary>
    /// School name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// School type (3=High school, 4=College/University, whatever your english).
    /// </summary>
    public int Type {get; set;}

    public Team Team;
}