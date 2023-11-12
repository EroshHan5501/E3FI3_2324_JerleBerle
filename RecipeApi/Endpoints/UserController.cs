using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Database.Extensions;
using RecipeApi.Exceptions;
using RecipeApi.Parameters;
using RecipeApi.Responses;
using RecipeApi.Responses.TransferObjects;

using System.Linq.Expressions;
using System.Security.Claims;

namespace RecipeApi.Endpoints;

[Authorize(Roles = "Admin, User")]
public class UserController : RecipeBaseController<User, UserParameter>
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
        Claim? idClaim = this.HttpContext.User.FindFirst(ClaimTypes.Sid);

        if (idClaim is null)
        {
            throw HttpException.NotFound("Can't identify current user!");
        }

        int id = int.Parse(idClaim?.Value!);

        User? currentUser = DbContext.Users
            .FirstOrDefault(u => u.Id == id);

        if (currentUser is null)
        {
            throw HttpException.NotFound("User does not exists!");
        }

        return Ok(currentUser); 
    }

    [HttpPost("create")]
    public override async Task<IActionResult> Create()
    {
        return Ok();
    }

    [HttpPost("update")]
    public override async Task<IActionResult> Update()
    {
        return Ok();
    }

    [HttpDelete("delete")]
    public override async Task<IActionResult> Delete()
    {

        return Ok();
    }
}
