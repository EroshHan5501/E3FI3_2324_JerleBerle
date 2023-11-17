﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RecipeApi.Authentication.TransferObjects;
using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Exceptions;
using RecipeApi.Extensions;
using RecipeApi.Helper;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace RecipeApi.Middlewares;

public class LoginMiddleware
{
    public RequestDelegate Next { get; set; }

    public LoginMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context, RecipeDbContext dbContext)
    {
        if (context.Request.Path != "/api/login/")
        {
            await Next.Invoke(context);
            return;
        }

        if (context.Request.Method != "Post") 
        {
            throw HttpException.MethodNotAllowed();
        }

        Credentials creds = await context.Request.BodyAsJsonAsync<Credentials>();

        if (!Validator.TryValidateObject(
                creds, 
                new (creds),
                new List<ValidationResult>()))
        {
            throw HttpException.BadRequest("Please provide valid credentials!");
        }

        User? user = dbContext.Users.FirstOrDefault(user => user.Email == creds.Email);

        if (user is null)
        {
            throw HttpException.BadRequest("Email or password are incorrect!");
        }

        // TODO: Create task for the hashing bug fix 
        string hashedPassword = HashHelper.GenerateSHA512Hash(creds.Password);

        if (user.Password != hashedPassword)
        {
            throw HttpException.BadRequest("Email or password are incorrect!");
        }

        ClaimsPrincipal principal = user.GeneratePrincipal();

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        context.Response.StatusCode = (int)HttpStatusCode.OK;

        return;
    }
}
