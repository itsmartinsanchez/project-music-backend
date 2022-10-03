namespace Song_Project.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Song_Project.Services;
using Song_Project.Models;
using Song_Project.Exceptions;

public class AuthorizeAdminFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the user is valid
        try 
        {
            if(context.HttpContext.Items.ContainsKey("user")) {
                User user = (User)context.HttpContext.Items["user"];

                if(!user.Role.Equals("admin")) {
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    payload.Add("message", "Only admin can access this");

                    context.Result = new UnauthorizedObjectResult(payload);
                }
            }
        } 
        catch(Exception e) 
        {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", "Unauthorized");

            context.Result = new UnauthorizedObjectResult(payload);
        }
    }
}