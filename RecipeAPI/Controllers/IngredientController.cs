using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IngredientModel>> PostIngredient(IngredientModel ingredient)
    {
	   DbContext.Ingredients.Add(ingredient);
	   await DbContext.SaveChangesAsync();
	   return Ok(ingredient);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> PutIngredient(int id, DTO dto)
    {
            var ingredient = await DbContext.Ingredients.FindAsync(id);
	    if (ingredient == null)
	    {
		    return BadRequest("No Entry for given Id.");
	    }

	    ingredient.Name = dto.Name;
	    var ingredientEntity = DbContext.Entry(ingredient);
	    DbContext.Entry(ingredient).State = EntityState.Modified;

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
