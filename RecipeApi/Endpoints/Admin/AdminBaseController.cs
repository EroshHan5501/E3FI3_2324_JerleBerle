using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

namespace RecipeApi.Endpoints.Admin;

[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin")]
public abstract class AdminBaseController<TEntity, TParameter, TCreate, TUpdate>
    : RecipeBaseController<TEntity, TParameter, TCreate, TUpdate>
    where TEntity : IKeyEntity where TParameter : ParameterBase<TEntity>
{
    public AdminBaseController(RecipeDbContext dbContext) 
        : base(dbContext)
    {
    }
}