using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;

namespace RecipeApi.Endpoints
{
    public class RecipeController : RecipeBaseController
    {
        public RecipeController(IDbContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {

            return Ok();
        }


    }
}
