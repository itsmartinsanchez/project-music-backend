namespace Song_Project.Models;
using Song_Project.Services;

public class Comment
{
    //private readonly IUserService userService;
    public Int32 Id {get; set;}
    public String Content {get; set;}
    public Int32 Rating {get; set;}
    public Int32 UserId {get; set;}
    public Int32 SongId {get; set;}
    //public String ?Username {get; set;}
    public Comment(){}
}