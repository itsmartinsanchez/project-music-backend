namespace Song_Project.Services;

using Song_Project.Models;

public interface IUserService 
{
    public User FindByUsername(string username);
    public User Register(string username, string password, string role);
    public User FindByToken(string token);
    public bool Exists(int id);
    public User FindByUserId(int userId);
    public void Logout(string username);
    public User CheckRole(string username);

}