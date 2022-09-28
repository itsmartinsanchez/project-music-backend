namespace Song_Project.Models;

public class Comments
{
    public Int32 Id {get; set;}
    public String Content {get; set;}
    public Int32 Rating {get; set;}
    public Int32 UserId {get; set;}
    public Int32 SongId {get; set;}
    public Comments(){}
}