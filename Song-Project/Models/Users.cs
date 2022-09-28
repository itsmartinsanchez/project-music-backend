namespace Song_Project.Models;

public class Users
{
    public Int32 Id { get; set; }
    public string Username { get; set; }
    public string EncryptedPassword { get; set; }
    public string ?Token { get; set; }
    public DateTime ?LastLogin { get; set; }
    public string ?Role { get; set; }
    public DateTime ?TokenExpiration { get; set; }
}