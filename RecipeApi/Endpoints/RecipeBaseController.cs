using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;

namespace RecipeApi.Endpoints
{
    [ApiController]
    public abstract class RecipeBaseController : ControllerBase
    {
        public IDbContext DbContext { get; }

        public RecipeBaseController(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

    }
}
