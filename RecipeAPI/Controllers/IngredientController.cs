using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers;

public class IngredientController : BaseController
{
    public IngredientController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<IngredientModel> GetAll() 
    {
        return DbContext.Ingredients.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientModel>> GetIngredients(int id)
    {
            if (DbContext.Ingredients == null)
            {
        	    return NotFound();
            }

            var ingredient = await DbContext.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
        	    return NotFound();
            }

            return ingredient;
    }

    [HttpGet("rels/{id}")]
    public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    {
	var liste = DbContext.RiuRels.Where(x => x.IngredientId == id).ToList();
	return liste;
    }

    [HttpPost]
    public async Task<ActionResult<RecipeModel>> PostIngredient(IngredientModel ingredient)
    {
	   DbContext.Ingredients.Add(ingredient);
	   await DbContext.SaveChangesAsync();
	   return Ok(ingredient);
    }
}
