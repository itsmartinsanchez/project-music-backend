namespace Song_Project.Services;

using Song_Project.Models;

public interface IUserService 
{
    public Users FindByUsername(string username);
    public Users Register(string username, string password, string role);
    public Users FindByToken(string token);
    public bool Exists(int id);
}