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

    public List<MeasureUnit> MeasureUnits { get; set; } = new List<MeasureUnit>();

    public List<Recipe> Recipes { get; set; } = new List<Recipe>();
}
