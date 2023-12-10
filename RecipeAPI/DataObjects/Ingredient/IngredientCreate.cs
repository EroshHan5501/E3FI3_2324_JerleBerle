using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DataObjects.Ingredient;

public class IngredientCreate
{
    [StringLength(100)]
    public string Name { get; set; }

    public int Amount { get; set; } 

    public string UnitOfMeasurement { get; set; }
}
