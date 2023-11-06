using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;

namespace RecipeApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public abstract class RecipeBaseController : ControllerBase
{
    public RecipeDbContext DbContext { get; set; }  

    public RecipeBaseController(RecipeDbContext dbContext)
    {
        DbContext = dbContext;
    }

}
