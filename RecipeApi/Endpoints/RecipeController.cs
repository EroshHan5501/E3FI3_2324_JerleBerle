using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Database.Extensions;
using RecipeApi.Parameters;
using RecipeApi.Responses;
using RecipeApi.Responses.TransferObjects;

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

        PagedEntityResponse<RecipeResponseObject> results = await query
            .Select(recipe => new RecipeResponseObject(recipe, false))
            .ToPageAsync(parameter.PageIndex, parameter.PageSize);

        return Ok(results);
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
