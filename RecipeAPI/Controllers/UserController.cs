using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RecipeApi.Helper;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using RecipeAPI.DataObjects.Users;
using RecipeAPI.Exceptions;

using System.Security.Claims;

namespace RecipeAPI.Controllers;

[Authorize(Roles = "Admin, User")]
public class UserController : BaseController
{
    public UserController(AppDbContext dbContext)
        : base(dbContext) { }

    [HttpGet("current")]
    public async Task<IActionResult> GetSingle()
    {
        UserModel currentUser = this.CurrentUser;

        UserResponseObject obj = new(currentUser, true);

        return Ok(obj);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] UserRegister register)
    {
        if (DbContext.Users.Any(user => user.Email == register.Email) ||
            DbContext.Users.Any(user => user.Username == register.Username))
        {
            throw HttpException.BadRequest("Email or username already exist!");
        }

        string hashedPassword = HashHelper.GenerateSHA512Hash(register.Password);

        UserModel user = new UserModel(register.Username, register.Email, hashedPassword);

        EntityEntry<UserModel> createdUser = DbContext.Users.Add(user);

        await DbContext.SaveChangesAsync();

        ClaimsPrincipal principal = createdUser.Entity.GeneratePrincipal();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UserUpdate update)
    {
        UserModel currentUser = this.CurrentUser;

        if (DbContext.Users.Any(user => user.Username == update.Username) &&
            currentUser.Username != update.Username)
        {
            throw HttpException.BadRequest(
                $"Username {update.Username} is already taken!");
        }

        if (DbContext.Users.Any(user => user.Email == update.Email) &&
            currentUser.Email != update.Email)
        {
            throw HttpException.BadRequest(
                $"Email {update.Email} is already taken!");
        }

        currentUser.Update(update);

        DbContext.Users.Update(currentUser);

        await DbContext.SaveChangesAsync();

        await this.HttpContext.SignOutAsync();

        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword updatePassword)
    {
        UserModel currentUser = this.CurrentUser;
        string hashOld = HashHelper.GenerateSHA512Hash(updatePassword.OldPassword);

        if (hashOld != currentUser.Password)
        {
            throw HttpException.BadRequest("Old password was incorrect!");
        }

        if (!updatePassword.IsConfirmed())
        {
            throw HttpException.BadRequest("Passwords does not match!");
        }

        currentUser.UpdatePassword(updatePassword.NewPassword);

        DbContext.Users.Update(currentUser);

        await DbContext.SaveChangesAsync();

        await this.HttpContext.SignOutAsync();

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        UserModel user = this.CurrentUser;

        DbContext.Users.Remove(user);

        await DbContext.SaveChangesAsync();

        await this.HttpContext.SignOutAsync();

        return Ok();
    }
}
