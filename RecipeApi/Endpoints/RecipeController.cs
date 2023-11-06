using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

namespace RecipeApi.Endpoints;

public class RecipeController : RecipeBaseController<Recipe, RecipeParameter>
{
    public RecipeController(RecipeDbContext dbContext)
        : base(dbContext)
    {

    }

    [HttpGet]
    public override async Task<IActionResult> Get([FromQuery]RecipeParameter parameter)
    {
        IEnumerable<Func<Recipe, bool>> filters = parameter.ParseTo();

        IQueryable<Recipe> query = DbContext.Recipes.AsQueryable();

        foreach (Func<Recipe, bool> filter in filters)
        {
            query = query.Where(filter).AsQueryable();
        }

        return Ok(query);
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
