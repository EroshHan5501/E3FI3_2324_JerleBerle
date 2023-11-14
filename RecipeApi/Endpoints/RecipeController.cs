using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Database.Extensions;
using RecipeApi.DataObjects.Recipe;
using RecipeApi.Exceptions;
using RecipeApi.Parameters;
using RecipeApi.Responses;
using RecipeApi.Responses.TransferObjects;

using System.Linq.Expressions;

namespace RecipeApi.Endpoints;

[Authorize(Roles = "User, Admin")]
public class RecipeController : RecipeBaseController<Recipe, RecipeParameter, RecipeCreate, RecipeUpdate>
{
    public RecipeController(RecipeDbContext dbContext)
        : base(dbContext)
    {

    }

    [HttpGet]
    public override async Task<IActionResult> Get([FromQuery]RecipeParameter parameter)
    {
        IEnumerable<Expression<Func<Recipe, bool>>> filters = parameter.ParseTo();

        IQueryable<Recipe> query = this.DbContext.Recipes;

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
    public override async Task<IActionResult> Create(RecipeCreate create)
    {
        IEnumerable<Ingredient> ingreds = DbContext.Ingredients
            .Where(ingred => create.IngredientIds.Contains(ingred.Id))
            .ToList();

        if (ingreds.Count() == 0)
        {
            throw HttpException.BadRequest("Ingredients does not exist!");
        }

        Recipe recipe = new Recipe(
            create.Title,
            create.Description,
            create.ImageUrl,
            this.CurrentUser,
            ingreds);
        
        DbContext.Recipes.Add(recipe);  

        await DbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("update")]
    public override async Task<IActionResult> Update(RecipeUpdate update)
    {
        return Ok();
    }

    [HttpDelete("delete")]
    public override async Task<IActionResult> Delete()
    {

        return Ok();
    }
}
