using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi.Database.Entities;

[Table("recipe")]
public class Recipe : IKeyEntity
{
    [Key]
    [Column("recipeId")]
    public int Id { get; set; }

    public string Title { get; set; }

    [Column("createdAt")]
    public DateTime StartedAt { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
    
    public List<IngredientRecipe> Ingredients { get; set; }
        = new List<IngredientRecipe>();
    
}
