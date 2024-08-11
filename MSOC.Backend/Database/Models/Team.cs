namespace MSOC.Backend.Database.Models
{
    /// <summary>
    /// Representing a team.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Team ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Team name, usually the school name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// idk.
        /// </summary>
        public int SchoolId { get; set; }
        
        /// <summary>
        /// idk.
        /// </summary>
        public int Group { get; set; }
        
        /// <summary>
        /// Members associated with this team.
        /// </summary>
        public ICollection<Player> Members = new List<Player>();
    }
}