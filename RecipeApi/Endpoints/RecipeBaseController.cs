using Microsoft.AspNetCore.Mvc;
using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

namespace RecipeApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public abstract class RecipeBaseController<TEntity, TParameter> : ControllerBase
    where TEntity : IKeyEntity where TParameter : ParameterBase<TEntity>
{
    public RecipeDbContext DbContext { get; set; }  

    public RecipeBaseController(RecipeDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract Task<IActionResult> Get(TParameter parameter);

    public abstract Task<IActionResult> Create();

    public abstract Task<IActionResult> Update();

    public abstract Task<IActionResult> Delete();

}
