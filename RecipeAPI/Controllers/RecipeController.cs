using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;

namespace RecipeAPI.Controllers;

public class RecipeController : BaseController
{
    public RecipeController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<RecipeModel> GetRecipes()
    {
        return DbContext.Recipes.ToList();
    }
}
