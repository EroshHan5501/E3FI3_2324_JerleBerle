using Microsoft.Extensions.Configuration.UserSecrets;

using RecipeApi.Database.Entities;

using System.Text.Json.Serialization;

namespace RecipeApi.Responses.TransferObjects;

public class UserResponseObject
{
    public int Id { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<RecipeResponseObject>? Recipes { get; set; }

    public UserResponseObject(User user, bool includeRecipes=false)
    {
        Id = user.Id;
        Username = user.Username;
        Role = user.Role;
        Recipes = includeRecipes ? user.Recipes.Select(x => new RecipeResponseObject(x, false)).ToList() : default;
    }
}
