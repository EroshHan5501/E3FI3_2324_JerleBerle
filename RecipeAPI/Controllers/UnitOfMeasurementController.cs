using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers;

public class UnitOfMeasurementController : BaseController
{    
    public UnitOfMeasurementController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<UnitOfMeasurementModel> Get()
    {
        return DbContext.UnitsOfMeasurement.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitOfMeasurementModel>> GetUnitOfMeasurement(int id)
    {
            if (DbContext.UnitsOfMeasurement == null)
            {
        	    return NotFound();
            }

            var unitOfMeasurement = await DbContext.UnitsOfMeasurement.FindAsync(id);

            if (unitOfMeasurement == null)
            {
        	    return NotFound();
            }

            return unitOfMeasurement;
    }

    [HttpGet("rels/{id}")]
    public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    {
	var liste = DbContext.RiuRels.Where(x => x.UnitOfMeasurementId == id).ToList();
	return liste;
    }

    [HttpPost]
    public async Task<ActionResult<UnitOfMeasurementModel>> PostUnitOfMeasurement(UnitOfMeasurementModel unitOfMeasurement)
    {
	   DbContext.UnitsOfMeasurement.Add(unitOfMeasurement);
	   await DbContext.SaveChangesAsync();
	   return Ok(unitOfMeasurement);
    }
}
