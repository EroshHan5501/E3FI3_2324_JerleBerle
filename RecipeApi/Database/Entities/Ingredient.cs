using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public List<IngredientRecipe> Recipes { get; set; } = new List<IngredientRecipe>();

    [NotMapped]
    [JsonPropertyName("recipes")]
    public List<Recipe> RecipeMapping => Recipes.Select(x => x.Recipe).ToList();

    public Ingredient()
    {

    }

    public Ingredient(string name, Unit unit, Amount amount)
    {
        Name = name;
        UnitId = unit.Id;
        Unit = unit;
        AmountId = amount.Id;
        Amount = amount;    
    }
}
