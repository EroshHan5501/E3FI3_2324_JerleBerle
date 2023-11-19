using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IngredientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<IngredientModel> GetAll() 
        {
            return _context.Ingredients.ToList();
        }
    }
}
