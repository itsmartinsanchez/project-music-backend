namespace Song_Project.Models;


public class Artists
{
    public Int32 Id {get; set;}
    public String Name {get; set;}
    public Artists()
    {
        
    }
    public Artists (int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}