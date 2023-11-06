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

    public User User { get; set; }
    
    public List<Ingredient> Ingredients { get; set; }
        = new List<Ingredient>();
    
}
