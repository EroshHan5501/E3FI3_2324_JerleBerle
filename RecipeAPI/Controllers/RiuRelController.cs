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

    // [HttpGet("{id}")]
    // public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    // {
    //     var liste = DbContext.RiuRels.Where(x => x.RecipeId == id).ToList();
    //     return liste;
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    // {
    //     var liste = DbContext.RiuRels.Where(x => x.IngredientId == id).ToList();
    //     return liste;
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    // {
    //     var liste = DbContext.RiuRels.Where(x => x.UnitOfMeasurementId == id).ToList();
    //     return liste;
    // }
}
