
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RecipeApi.Helper;
using RecipeAPI.Database.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using RecipeAPI.Authentication.DataObjects;
using RecipeAPI.Exceptions;
using RecipeAPI.Extensions;
using RecipeAPI.Database;

namespace RecipeAPI.Authentication.Middleware;

public class LoginMiddleware
{
    public RequestDelegate Next { get; set; }

    public LoginMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        if (context.Request.Path != "/api/login/")
        {
            await Next.Invoke(context);
            return;
        }

        if (context.Request.Method != "POST")
        {
            throw HttpException.MethodNotAllowed();
        }

        Credentials creds = await context.Request.BodyAsJsonAsync<Credentials>();

        if (!Validator.TryValidateObject(
                creds,
        new(creds),
                new List<ValidationResult>()))
        {
            throw HttpException.BadRequest("Please provide valid credentials!");
        }

        UserModel? user = dbContext.Users.FirstOrDefault(user => user.Email == creds.Email);

        if (user is null)
        {
            throw HttpException.BadRequest("Email or password are incorrect!");
        }

        string hashedPassword = HashHelper.GenerateSHA512Hash(creds.Password);

        if (user.Password != hashedPassword)
        {
            throw HttpException.BadRequest("Email or password are incorrect!");
        }

        ClaimsPrincipal principal = user.GeneratePrincipal();

        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            principal);

        context.Response.StatusCode = (int)HttpStatusCode.OK;

        return;
    }
}
