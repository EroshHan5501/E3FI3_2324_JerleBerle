using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database.Models;
using RecipeAPI.DataObjects.Users;
using RecipeAPI.Exceptions;
using RecipeApi.Helper;
using RecipeAPI.Database;

namespace RecipeAPI.Controllers.Admin;

[Route("api/admin/[controller]")]
public class UserController : AdminBaseController
{
    public UserController(AppDbContext dbContext) : base(dbContext)
    {

    }

    //public override async Task<IActionResult> Get()
    //{
    //    // We want to exclude the recipes on database request 
    //    IEnumerable<Expression<Func<UserModel, bool>>> filters = parameter.ParseTo();

    //    IQueryable<UserModel> query = DbContext.Users;

    //    foreach (Expression<Func<UserModel, bool>> filter in filters)
    //    {
    //        query = query.Where(filter);
    //    }

    //    PagedEntityResponse<UserResponseObject> results = await query
    //        .Select(user => new UserResponseObject(user, false)) // TODO: Rethink how we can exclude recipes from loading when requesting from database
    //        .ToPageAsync(parameter.PageIndex, parameter.PageSize);

    //    // TODO: Use same code base 

    //    return Ok(results);
    //}

    //[HttpGet("roles")]
    //public async Task<IActionResult> GetRoles()
    //{
    //    IQueryable<string> query = DbContext.Users
    //        .GroupBy(user => user.Role);

    //    PagedEntityResponse<string> result = await query.ToPageAsync(1, 20);

    //    return Ok(result);
    //}

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

    [HttpPost("update-password")]
    public async Task<IActionResult> Update(UpdatePassword update)
    {
        // Just update the password and notify the user via email 

        // Generates a default password and sends this within the email
        // https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/

        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUserBy(UserDelete delete)
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