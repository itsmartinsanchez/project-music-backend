namespace Song_Project.Controllers;

using Song_Project.Models;
using Song_Project.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Song_Project.Operations.Users;
using Song_Project.Exceptions;
using Song_Project.Filters;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly AuthenticationService _authenticationService;
    private readonly ValidateRegister _validateRegister;

    public UsersController(
        ILogger<UsersController> logger, 
        IUserService userService, 
        ValidateRegister validateRegister, 
        AuthenticationService authenticationService)
    {
        _logger = logger;
        _userService = userService;
        _validateRegister = validateRegister;
        _authenticationService = authenticationService;
    }

    [HttpPut("{username}/logout")]
    //[ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult Logout(string username)
    {
        User user = (User)HttpContext.Items["user"];
        _userService.Logout(username);

        Dictionary<string, string> msg = new Dictionary<string, string>();
        msg["message"] = "Successfully logged out";

        return Ok(msg);
    }

    [HttpPost("login")] // POST /users/login
    public ActionResult Login([FromBody] object payload)
    {
        try
        {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            string username = hash["username"].ToString();
            string password = hash["password"].ToString();

            string token = _authenticationService.Login(username, password);

            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["token"] = token;

            return Ok(msg);
        }
        catch(InvalidLoginException e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Invalid login";
            
            System.Console.WriteLine(e.StackTrace);

            return StatusCode(StatusCodes.Status404NotFound, msg);
        }
        catch(Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";
            _logger.LogInformation(e.Message);
            _logger.LogInformation(e.StackTrace);
            System.Console.WriteLine(e.StackTrace);

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }


    [HttpPost("register")] // POST /users/register
    public ActionResult Register([FromBody]object payload)
    {
        try
        {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateRegister.InitializeParameters(hash);
            _validateRegister.run();

            if(_validateRegister.HasErrors) {
                return UnprocessableEntity(_validateRegister.Payload);
            } else {
                return Ok(_userService.Register(hash["username"].ToString(), hash["password"].ToString(), hash["role"].ToString()));
            }
        }
        catch(Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
    
    [HttpGet("{username}")]
    public ActionResult GetUserRole(string username)
    {
        try
        {
            User user = _userService.CheckRole(username);

            return Ok(user);
        }
        catch (Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
}