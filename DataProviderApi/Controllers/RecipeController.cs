﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataProviderApi.Models;

namespace DataProviderApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly RecipeContext _context;

    public RecipesController(RecipeContext context)
    {
        _context = context;
    }

    // GET: api/Recipes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipeItems()
    {
        if (_context.RecipeItems == null)
        {
            return NotFound();
        }
        return await _context.RecipeItems.ToListAsync();
    }

    // GET: api/Recipes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipe(int id)
    {
        if (_context.RecipeItems == null)
        {
            return NotFound();
        }
        var recipe = await _context.RecipeItems.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return recipe;
    }

    // PUT: api/Recipes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return BadRequest();
        }

        _context.Entry(recipe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Recipes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
    {
        if (_context.RecipeItems == null)
        {
            return Problem("Entity set 'RecipeContext.RecipeItems'  is null.");
        }
        _context.RecipeItems.Add(recipe);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
    }

    // DELETE: api/Recipes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        if (_context.RecipeItems == null)
        {
            return NotFound();
        }
        var recipe = await _context.RecipeItems.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }

        _context.RecipeItems.Remove(recipe);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RecipeExists(int id)
    {
        return (_context.RecipeItems?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

