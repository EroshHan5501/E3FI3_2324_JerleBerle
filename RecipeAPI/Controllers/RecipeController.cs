using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

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

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> PutRecipe(int id, DTO dto)
    {
            var recipe = await DbContext.Recipes.FindAsync(id);
	    if (recipe == null)
	    {
		    return BadRequest("No Entry for given Id.");
	    }
	    if(this.CurrentUser.Id != recipe.UserId && this.CurrentUser.Role != Role.Admin)
	    {
		    return Unauthorized("User not authorized.");
	    }

	    recipe.Name = dto.Name;
	    var recipeEntity = DbContext.Entry(recipe);
	    DbContext.Entry(recipe).State = EntityState.Modified;

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
