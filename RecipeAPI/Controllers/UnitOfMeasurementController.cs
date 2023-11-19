using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public UnitOfMeasurementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UnitOfMeasurementModel> Get()
        {
            return _context.UnitsOfMeasurement.ToList();
        }
    }
}
