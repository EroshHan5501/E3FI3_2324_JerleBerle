﻿using Microsoft.AspNetCore.Mvc;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
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

	    DbContext.Recipes.Remove(recipe);
	    await DbContext.SaveChangesAsync();

	    return NoContent();
    }
}
