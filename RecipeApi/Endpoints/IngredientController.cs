using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.DataObjects.Ingredient;
using RecipeApi.Parameters;

namespace RecipeApi.Endpoints;

public class IngredientController : RecipeBaseController<Ingredient, IngredientParameter, IngredientCreate, IngredientUpdate>
{
    public IngredientController(RecipeDbContext dbContext) : base(dbContext)
    {
    }

    public override Task<IActionResult> Get(IngredientParameter parameter)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> Create(IngredientCreate body)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> Update(IngredientUpdate body)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> Delete()
    {
        throw new NotImplementedException();
    }

}
