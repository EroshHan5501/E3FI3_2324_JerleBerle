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
    public async Task<ActionResult<IngredientModel>> PostIngredient(IngredientModel ingredient)
    {
	   DbContext.Ingredients.Add(ingredient);
	   await DbContext.SaveChangesAsync();
	   return Ok(ingredient);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteIngredient(int id)
    {
	    if(DbContext.Ingredients == null)
	    {
		    return NotFound();
	    }
	    
	    var ingredient = await DbContext.Ingredients.FindAsync(id);
	    if(ingredient == null)
	    {
		    return NotFound();
	    }

	    var liste = DbContext.RiuRels.Where(x => x.IngredientId == id).ToList();
	    if(liste.Count() > 0)
	    {
		    return BadRequest("Deleting this Entry would violate the referential Integrity of a Relation.\nPlease consider doing a complete Delete by attaching /complete to the URI.");
	    }
	    
	    DbContext.Ingredients.Remove(ingredient);
	    await DbContext.SaveChangesAsync();

	    return Ok($"Successfully deleted Entry: {ingredient.Name}");
    }

    [HttpDelete("{id}/complete")]
    public async Task<ActionResult<string>> DeleteWithRelations(int id)
    {
	    if(DbContext.Ingredients == null)
	    {
		    return NotFound("Given Endpoint has no Entries.");
	    }
	    
	    var ingredient = await DbContext.Ingredients.FindAsync(id);
	    if(ingredient == null)
	    {
		    return NotFound("Endpoint has no Entry with the following Id: {id}.");
	    }

	    var liste = DbContext.RiuRels.Where(x => x.IngredientId == id).ToList();
	    foreach(var relation in liste)
	    {
		    DbContext.RiuRels.Remove(relation);
	    }

	    await DbContext.SaveChangesAsync();

	    DbContext.Ingredients.Remove(ingredient);
	    await DbContext.SaveChangesAsync();

	    return Ok($"Successfully deleted {ingredient.Name} and all its References.");
    }
}
