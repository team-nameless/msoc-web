namespace MSOC.Backend.Database.Models
{
    public class School
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Alias {get; set;}
        //alias could be a json array, but is now an optional thing
    }
}