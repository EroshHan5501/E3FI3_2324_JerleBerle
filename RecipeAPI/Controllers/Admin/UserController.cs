using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database.Models;
using RecipeAPI.DataObjects.Users;
using RecipeAPI.Exceptions;
using RecipeApi.Helper;
using RecipeAPI.Database;
using RecipeAPI.Responses;
using RecipeApi.Parameters;
using RecipeAPI.Extensions;

namespace RecipeAPI.Controllers.Admin;

public class UserController : AdminBaseController
{
    public UserController(AppDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<IActionResult> Get([FromQuery] UserParameter parameter)
    {
        IQueryable<UserModel> query = DbContext.Users
            .Where(x => parameter.Username != null ? x.Username.Contains(parameter!.Username) : true);

        PagedEntityResponse<UserResponseObject> results = await query
            .Select(user => new UserResponseObject(user, false))
            .ToPageAsync(parameter.PageIndex, parameter.PageSize);

        return Ok(results);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(ExtendedUserRegister register)
    {
        // Creating a new user account just like in the normal controller but with role set 
        if (DbContext.Users.Any(user => user.Email == register.Email) ||
            DbContext.Users.Any(user => user.Username == register.Username))
        {
            throw HttpException.BadRequest("Email or username already exist!");
        }

        string hashedPassword = HashHelper.GenerateSHA512Hash(register.Password);

        UserModel user = new UserModel(
            register.Username,
            register.Email,
            hashedPassword,
            register.Role);

        DbContext.Users.Add(user);

        await DbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("update-role")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRole data)
    {
        UserModel? user = DbContext.Users
            .FirstOrDefault(x => x.Email == data.Email);

        if (user is null)
        {
            throw HttpException.NotFound(
                $"User with email {data.Email} does not exists!");
        }

        user.Role = data.Role;

        return Ok();
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword update)
    {
        // Just update the password and notify the user via email 

        // Generates a default password and sends this within the email
        // https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/

        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUserBy([FromQuery]UserDelete delete)
    {
        UserModel? user = DbContext.Users
            .FirstOrDefault(user => user.Email == delete.Email);

        if (user is null)
        {
            throw HttpException.NotFound($"User with email {delete.Email} does not exists!");
        }

        if (user.Role == Role.Admin)
        {
            throw HttpException.Forbidden("You can't delete another admin user. Please remove his current role before deleting the user!");
        }

        if (user == CurrentUser)
        {
            throw HttpException.Forbidden("You can't delete your own account!");
        }

        DbContext.Users.Remove(user);

        await DbContext.SaveChangesAsync();

        return Ok();
    }
}