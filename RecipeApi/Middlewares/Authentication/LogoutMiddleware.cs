using Microsoft.AspNetCore.Authentication;

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
        if (context.Request.Path != "/api/logout")
        {
            await Next(context);
            return;
        }

        await context.SignOutAsync();
    }
}
