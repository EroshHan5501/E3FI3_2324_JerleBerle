using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi.Database.Entities;

[Table("ingredient")]
public class Ingredient : IKeyEntity
{
    [Key]
    [Column("ingredientId")]
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int UnitId { get; set; }

    public Unit Unit { get; set; }

    public int AmountId { get; set; }

    public Amount Amount { get; set; }

    public List<IngredientRecipe> Recipes { get; set; } = new List<IngredientRecipe>(); 
}
