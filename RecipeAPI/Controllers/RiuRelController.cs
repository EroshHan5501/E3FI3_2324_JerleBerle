using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiuRelController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RiuRelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<RiuRelModel> Get()
        {
            return _context.RiuRels.ToList();
        }
    }
}
