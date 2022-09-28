namespace Song_Project.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Song_Project.Services;
using Song_Project.Models;

class AuthenticationFilter : Attribute, IAuthorizationFilter
{

    private readonly IUserService _userService;

    public AuthenticationFilter(IUserService userService)
    {
        _userService = userService;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isAuthenticated = false;

        if(context.HttpContext.Request.Headers.ContainsKey("X-AWESOME-AUTHENTICATION")) {

            string tokenValue = context.HttpContext.Request.Headers["X-AWESOME-AUTHENTICATION"].ToString();

            Users user = _userService.FindByToken(tokenValue);

            if(user != null) {
                isAuthenticated = true;

                context.HttpContext.Items.Add("user", user);
            }
        }

        if(!isAuthenticated) {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", "Unauthenticated");

            context.Result = new UnauthorizedObjectResult(payload);
        }
    }
}