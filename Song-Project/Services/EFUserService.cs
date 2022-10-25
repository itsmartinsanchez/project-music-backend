namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Data;
using System.Security.Cryptography;
using System.Text;
using Song_Project.Operations.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

public class EFUserService : IUserService
{
    private readonly DataContext _dataContext;

    public EFUserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Exists(int id)
    {
        return _dataContext.Comment.SingleOrDefault(c => c.Id == id) != null;
    }

    public User FindByToken(string token)
    {
        return _dataContext.User.SingleOrDefault(u => u.Token.Equals(token));
    }
    public User CheckRole(string username)
    {
        User user  = _dataContext.User.SingleOrDefault(u => u.Username == username);

        return user;
    }

    public User FindByUserId(int userId)
    {
        if(userId != null){
            var uid = new SqlParameter(
                "userId",
                userId
            );
            
            User u = _dataContext.User
            .FromSqlRaw("SELECT * FROM dbo.[User] WHERE Id=@userId", uid)
            .SingleOrDefault();

            return u;
        }

        return null;
    }

    public User FindByUsername(string username)
    {
        return _dataContext.User.SingleOrDefault(u => u.Username.Equals(username));
    }

    public void Logout(string username)
        {
            User user = FindByUsername(username);

            if (user != null)
            {
                user.Token = null;
                _dataContext.SaveChanges();
            }

            
        }

    public User Register(string username, string password, string role)
    {
        HashPassword hasher = new HashPassword(password);
        hasher.run();

        string encryptedPassword = hasher.Hash;

        User user = new User() {
            Username = username,
            EncryptedPassword = encryptedPassword,
            Role = role
        };

        _dataContext.User.Add(user);
        _dataContext.SaveChanges();

        return user;
    }
}