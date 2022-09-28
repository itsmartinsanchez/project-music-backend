namespace Song_Project.Operations.Users;

using Song_Project.Models;
using System.Diagnostics;

class GenerateToken
{
    public string Token { get; private set; }

    private Users _users;

    public GenerateToken(Users users)
    {
        _users = users;
    }

    public void run()
    {
        Token = (Guid.NewGuid()).ToString();
    }
}