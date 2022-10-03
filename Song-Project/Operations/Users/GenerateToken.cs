namespace Song_Project.Operations.Users;

using Song_Project.Models;
using System.Diagnostics;

class GenerateToken
{
    public string Token { get; private set; }

    private User _users;

    public GenerateToken(User users)
    {
        _users = users;
    }

    public void run()
    {
        Token = (Guid.NewGuid()).ToString();
    }
}