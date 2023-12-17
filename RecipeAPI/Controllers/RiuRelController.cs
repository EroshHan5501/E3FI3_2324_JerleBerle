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
}
