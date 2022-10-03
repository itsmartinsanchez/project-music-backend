namespace Song_Project.Models;


public class Artist
{
    public Int32 Id {get; set;}
    public String Name {get; set;}
    public Artist()
    {
        
    }
    public Artist (int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}