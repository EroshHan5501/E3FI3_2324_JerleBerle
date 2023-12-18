using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;

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

    [HttpDelete("{id}")]
    public async Task<ActionResult<RiuRelModel>> DeleteRiuRel(int id)
    {
	    if(DbContext.RiuRels == null)
	    {
		    return NotFound();
	    }
	    
	    var relation = await DbContext.RiuRels.FindAsync(id);
	    if(relation == null)
	    {
		    return NotFound();
	    }

	    DbContext.RiuRels.Remove(relation);
	    await DbContext.SaveChangesAsync();

	    return Ok(relation);
    }
}
