using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Exceptions;
using RecipeApi.Parameters;
using System.Security.Claims;

namespace RecipeApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public abstract class RecipeBaseController<TEntity, TParameter, TCreate, TUpdate> : ControllerBase
    where TEntity : IKeyEntity where TParameter : ParameterBase<TEntity>
{
    public RecipeDbContext DbContext { get; set; }  

    public User CurrentUser
    {
        get
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

            return currentUser;
        }
    }

    public RecipeBaseController(RecipeDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract Task<IActionResult> Get(TParameter parameter);

    public abstract Task<IActionResult> Create(TCreate body);

    public abstract Task<IActionResult> Update(TUpdate body);

    public abstract Task<IActionResult> Delete();

}
