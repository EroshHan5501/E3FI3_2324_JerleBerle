using Microsoft.AspNetCore.Authentication;
using RecipeApi.Exceptions;

namespace RecipeApi.Middlewares.Authentication;

public class LogoutMiddleware
{
    public RequestDelegate Next { get; set; }   

    public LogoutMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path != "/api/logout/")
        {
            await Next(context);
            return;
        }

        if (context.Request.Method != "GET")
        {
            throw HttpException.MethodNotAllowed();
        }

        await context.SignOutAsync();
    }
}
