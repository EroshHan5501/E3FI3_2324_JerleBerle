using RecipeApi.Database.Entities;

using System.Text.Json.Serialization;

namespace RecipeApi.Responses.TransferObjects;

public class RecipeResponseObject
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserResponseObject? User { get; set; }

    public List<IngredientResponseObject> Ingredients { get; set; }

    public RecipeResponseObject(Recipe recipe)
    {
        Id = recipe.Id;
        Title = recipe.Title;
        CreatedAt = recipe.StartedAt;
        Description = recipe.Description;
        ImageUrl = recipe.ImageUrl;
        //User = recipe.User is not null ?  new UserResponseObject(recipe.User) : null;
        Ingredients = recipe.JsonIngredients.Select(x => new IngredientResponseObject(x)).ToList();
    }
}
