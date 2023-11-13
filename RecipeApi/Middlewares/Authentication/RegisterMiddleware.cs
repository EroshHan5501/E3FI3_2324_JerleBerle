using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeApi.Authentication.TransferObjects;
using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Exceptions;
using RecipeApi.Extensions;
using RecipeApi.Helper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace RecipeApi.Middlewares.Authentication;

public class RegisterMiddleware
{
    public RequestDelegate Next { get; set; }

    public RegisterMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(
        HttpContext context, 
        RecipeDbContext dbContext)
    {
        if (context.Request.Path != "/api/register/")
        {
            Next?.Invoke(context);
            return;
        }

        if (context.Request.Method != "POST")
        {
            throw HttpException.MethodNotAllowed();
        }

        Register register = await context.Request.BodyAsJsonAsync<Register>();
        if (!Validator.TryValidateObject(
                register, 
                new (register),
                new List<ValidationResult>()))
        {
            throw HttpException.BadRequest("Invalid credentials!");
        }

        if (dbContext.Users.Any(user => user.Email == register.Email) || 
            dbContext.Users.Any(user => user.Username == register.Username))
        {
            throw HttpException.BadRequest("Email or username already exist!");
        }

        string hashedPassword = HashHelper.GenerateSHA512Hash(register.Password);

        User user = new User(register.Username, register.Email, hashedPassword);

        EntityEntry<User> createdUser = dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync();

        ClaimsPrincipal principal = createdUser.Entity.GeneratePrincipal();

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        context.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}
