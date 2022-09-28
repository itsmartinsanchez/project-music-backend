namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Exceptions;
using Song_Project.Services;
using Song_Project.Operations.Users;
using Song_Project.Data;

public class AuthenticationService 
{
    private IUserService _userService;
    private DataContext _dataContext;

    public AuthenticationService(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    public void Logout(Users user)
    {
        user.Token = null;

        _dataContext.SaveChanges();
    }

    public string Login(string username, string password)
    {
        Users user = _userService.FindByUsername(username);

        if(user == null) {
            throw new InvalidLoginException();
        } else {
            HashPassword hasher = new HashPassword(password);
            hasher.run();

            string encryptedPassword = hasher.Hash;

            if(encryptedPassword.Equals(user.EncryptedPassword)) {
                GenerateToken generator = new GenerateToken(user);
                generator.run();

                user.Token = generator.Token;
                user.LastLogin = DateTime.Now;
                user.TokenExpiration = DateTime.Now.AddMinutes(5);

                _dataContext.SaveChanges();

                return user.Token;
            } else {
                throw new InvalidLoginException();
            }
        }
    }
}