namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Data;
using System.Security.Cryptography;
using System.Text;
using Song_Project.Operations.Users;

public class EFUserService : IUserService
{
    private readonly DataContext _dataContext;

    public EFUserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Exists(int id)
    {
        return _dataContext.Comments.SingleOrDefault(c => c.Id == id) != null;
    }

    public Users FindByToken(string token)
    {
        return _dataContext.Users.SingleOrDefault(u => u.Token.Equals(token));
    }

    public Users FindByUsername(string username)
    {
        return _dataContext.Users.SingleOrDefault(u => u.Username.Equals(username));
    }

    public Users Register(string username, string password, string role)
    {
        HashPassword hasher = new HashPassword(password);
        hasher.run();

        string encryptedPassword = hasher.Hash;

        Users user = new Users() {
            Username = username,
            EncryptedPassword = encryptedPassword,
            Role = role
        };

        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();

        return user;
    }
}