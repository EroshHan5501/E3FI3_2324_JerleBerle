using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers;

public class RecipeController : BaseController
{
    public RecipeController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<RecipeModel> GetRecipes()
    {
        return DbContext.Recipes.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeModel>> GetRecipes(int id)
    {
            if (DbContext.Recipes == null)
            {
        	    return NotFound();
            }

            var recipe = await DbContext.Recipes.FindAsync(id);

            if (recipe == null)
            {
        	    return NotFound();
            }

            return recipe;
    }

    [HttpGet("rels/{id}")]
    public async Task<ActionResult<IEnumerable<RiuRelModel>>> GetRels(int id)
    {
        var liste = DbContext.RiuRels.Where(x => x.RecipeId == id).ToList();
        return liste;
    }

    [HttpPost]
    public async Task<ActionResult<RecipeModel>> PostRecipe(DTO dto)
    {
	   RecipeModel newRecipeModel = new() { Name = dto.Name, UserId = this.CurrentUser.Id };
	   DbContext.Recipes.Add(newRecipeModel);
	   await DbContext.SaveChangesAsync();
	   //return CreatedAtAction("GetRecipes", new { id = newRecipeModel.Id }, newRecipeModel );
	   return Ok(newRecipeModel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteRecipe(int id)
    {
	    if(DbContext.Recipes == null)
	    {
		    return NotFound();
	    }
	    
	    var recipe = await DbContext.Recipes.FindAsync(id);
	    if(recipe == null)
	    {
		    return NotFound();
	    }

	    var liste = DbContext.RiuRels.Where(x => x.RecipeId == id).ToList();
	    if(liste.Count() > 0)
	    {
		    return BadRequest("Deleting this Entry would violate the referential Integrity of a Relation.\nPlease consider doing a complete Delete by attaching /complete to the URI.");
	    }
	    
	    DbContext.Recipes.Remove(recipe);
	    await DbContext.SaveChangesAsync();

	    return Ok($"Successfully deleted Entry: {recipe.Name}");
    }
    
    [HttpDelete("{id}/complete")]
    public async Task<ActionResult<string>> DeleteWithRelations(int id)
    {
	    if(DbContext.Recipes == null)
	    {
		    return NotFound("Given Endpoint has no Entries.");
	    }
	    
	    var recipe = await DbContext.Recipes.FindAsync(id);
	    if(recipe == null)
	    {
		    return NotFound("Endpoint has no Entry with the following Id: {id}.");
	    }

	    var liste = DbContext.RiuRels.Where(x => x.RecipeId == id).ToList();
	    foreach(var relation in liste)
	    {
		    DbContext.RiuRels.Remove(relation);
	    }

	    await DbContext.SaveChangesAsync();

	    DbContext.Recipes.Remove(recipe);
	    await DbContext.SaveChangesAsync();

	    return Ok($"Successfully deleted {recipe.Name} and all its References.");
    }
}
