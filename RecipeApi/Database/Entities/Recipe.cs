using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeApi.Database.Entities;

[Table("recipe")]
public class Recipe : IKeyEntity
{
    [Key]
    [Column("recipeId")]
    public int Id { get; set; }

    public string Title { get; set; } = null!;  

    [Column("createdAt")]
    public DateTime StartedAt { get; set; }

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    [JsonIgnore]
    public List<IngredientRecipe> Ingredients { get; set; }
        = new List<IngredientRecipe>();

    [NotMapped]
    [JsonPropertyName("ingredients")]
    public List<Ingredient> JsonIngredients => Ingredients.Select(x => x.Ingredient).ToList();

    // Constructor is necessary for EF Core to work the correct way
    public Recipe()
    {

    }

    public Recipe(
        string title,
        string description,
        string imageUrl,
        User user, 
        IEnumerable<Ingredient> create)
    {
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        StartedAt = DateTime.UtcNow;
        UserId = user.Id;
        User = user;

        foreach (Ingredient ingred in create)
        {
            IngredientRecipe mapping = new IngredientRecipe()
            {
                Ingredient = ingred,
                IngredientId = ingred.Id,
                RecipeId = Id,
                Recipe = this
            };

            Ingredients.Add(mapping);
        }
    }
}
