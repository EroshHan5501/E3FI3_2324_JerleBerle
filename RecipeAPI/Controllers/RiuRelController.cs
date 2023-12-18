using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeAPI.Controllers;

public class RiuRelController : BaseController
{
    public RiuRelController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<RiuRelModel> Get()
    {
        return DbContext.RiuRels.ToList();
    }

    [HttpPost]
    public async Task<ActionResult<RiuRelModel>> PostRiuRel(RiuRelModel relation)
    {
	   DbContext.RiuRels.Add(relation);
	   await DbContext.SaveChangesAsync();
	   return Ok(relation);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> PutRiuRel(int id, RelDTO dto)
    {
            var riuRel = await DbContext.RiuRels.FindAsync(id);
	    if (riuRel == null)
	    {
		    return BadRequest("No Entry for given Id.");
	    }

	    riuRel.Quantity = dto.Quantity;
	    var riuRelEntity = DbContext.Entry(riuRel);
	    DbContext.Entry(riuRel).State = EntityState.Modified;

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
