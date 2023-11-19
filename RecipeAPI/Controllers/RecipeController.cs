using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RecipeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<RecipeModel> GetRecipes()
        {
            return  _context.Recipes.ToList();
        }
    }
}
