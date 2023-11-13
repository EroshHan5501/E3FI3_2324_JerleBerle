using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeApi.Authentication.TransferObjects;
using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Database.Extensions;
using RecipeApi.Exceptions;
using RecipeApi.Extensions;
using RecipeApi.Helper;
using RecipeApi.Parameters;
using RecipeApi.Responses;
using RecipeApi.Responses.TransferObjects;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Claims;

namespace RecipeApi.Endpoints;

[Authorize(Roles = "Admin, User")]
public class UserController : RecipeBaseController<User, UserParameter, Register, Update>
{
    public UserController(RecipeDbContext dbContext) 
        : base(dbContext) { }

    [HttpGet]
    public override async Task<IActionResult> Get([FromQuery]UserParameter parameter)
    {
        IEnumerable<Expression<Func<User, bool>>> filters = parameter.ParseTo();

        IQueryable<User> query = DbContext.Users;

        foreach (Expression<Func<User, bool>> filter in filters)
        {
            query = query.Where(filter);
        }

        PagedEntityResponse<UserResponseObject> results = await query
            .Select(user => new UserResponseObject(user, true))
            .ToPageAsync(parameter.PageIndex, parameter.PageSize);

        return Ok(results);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetSingle()
    {
        User currentUser = this.CurrentUser;
        return Ok(currentUser); 
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public override async Task<IActionResult> Create([FromBody]Register register)
    { 
        if (DbContext.Users.Any(user => user.Email == register.Email) ||
            DbContext.Users.Any(user => user.Username == register.Username))
        {
            throw HttpException.BadRequest("Email or username already exist!");
        }

        string hashedPassword = HashHelper.GenerateSHA512Hash(register.Password);

        User user = new User(register.Username, register.Email, hashedPassword);

        EntityEntry<User> createdUser = DbContext.Users.Add(user);

        await DbContext.SaveChangesAsync();

        ClaimsPrincipal principal = createdUser.Entity.GeneratePrincipal();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok();
    }

    [HttpPost("update")]
    public override async Task<IActionResult> Update([FromBody]Update update)
    {
        if (!string.IsNullOrWhiteSpace(update.Username))
        {

        }
        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> UpdatePassword([FromBody]UpdatePassword updatePassword)
    {

    }

    [HttpDelete("delete")]
    public override async Task<IActionResult> Delete()
    {

        return Ok();
    }
}
