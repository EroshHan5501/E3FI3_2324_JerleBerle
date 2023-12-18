using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

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

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> PutUnitOfMeasurement(int id, DTO dto)
    {
            var unitOfMeasurement = await DbContext.UnitsOfMeasurement.FindAsync(id);
	    if (unitOfMeasurement == null)
	    {
		    return BadRequest("No Entry for given Id.");
	    }

	    unitOfMeasurement.Name = dto.Name;
	    var unitOfMeasurementEntity = DbContext.Entry(unitOfMeasurement);
	    DbContext.Entry(unitOfMeasurement).State = EntityState.Modified;

	    try
	    {
		    await DbContext.SaveChangesAsync();
	    }
	    catch (DbUpdateConcurrencyException)
	    {
		    throw;
	    }

	    return Ok();
    }
}
