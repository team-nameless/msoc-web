namespace MSOC.Backend.Database.Models
{
    public class Player
    {
        public ulong Id {get; set;}
        public string Maimai_name {get; set;}
        public int Rating {get; set;}
        public int SchoolId {get; set;}
        public bool IsLeader {get; set;}
        public float Sub1 {get; set;}
        public float Sub2 {get; set;}
        public float Sub3 {get; set;}
        public float SubTotal {get; set;}
    }
}