using RecipeApi.Database.Entities;

namespace RecipeApi.Responses.TransferObjects;

public class UserResponseObject
{
    public int Id { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }

    public List<RecipeResponseObject> Recipes { get; set; }

    public UserResponseObject(User user)
    {
        Id = user.Id;
        Username = user.Username;
        Role = user.Role;
        Recipes = user.Recipes.Select(x => new RecipeResponseObject(x)).ToList();
    }
}
