using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;

namespace RecipeApi.Endpoints;

public class RecipeController : RecipeBaseController
{
    public RecipeController(RecipeDbContext dbContext)
        : base(dbContext)
    {

    }

    [HttpGet]
    public async Task<IActionResult> GetRecipes()
    {
        var result = DbContext.Users.ToList();
        return Ok(result);
    }
}
