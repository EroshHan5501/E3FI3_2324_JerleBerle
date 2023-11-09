using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

using System.Linq.Expressions;

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
        IEnumerable<Expression<Func<Recipe, bool>>> filters = parameter.ParseTo();

        IQueryable<Recipe> query = DbContext.Recipes;

        foreach (Expression<Func<Recipe, bool>> filter in filters)
        {
            query = query.Where(filter);
        }

        var includeQuery = query
            .Include(recipe => recipe.Ingredients);

        return Ok(includeQuery);
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
