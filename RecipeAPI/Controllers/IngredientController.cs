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
}
