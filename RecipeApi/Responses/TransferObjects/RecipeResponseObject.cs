using RecipeApi.Database.Entities;

namespace RecipeApi.Responses.TransferObjects;

public class RecipeResponseObject
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public UserResponseObject User { get; set; }

    public List<IngredientResponseObject> Ingredients { get; set; }

    public RecipeResponseObject(Recipe recipe, bool includeUser)
    {
        Id = recipe.Id;
        Title = recipe.Title;
        CreatedAt = recipe.StartedAt;
        Description = recipe.Description;
        ImageUrl = recipe.ImageUrl;
        User = includeUser ? 
            new UserResponseObject(recipe.User) : 
            new UserResponseObject(recipe.User, false) ;
        Ingredients = recipe.JsonIngredients.Select(x => new IngredientResponseObject(x)).ToList();
    }

}
