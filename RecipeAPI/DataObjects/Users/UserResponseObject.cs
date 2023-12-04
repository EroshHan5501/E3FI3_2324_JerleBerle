using RecipeAPI.Database.Models;

namespace RecipeAPI.DataObjects.Users;

public class UserResponseObject
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public Role Role { get; set; }

    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    //public List<RecipeResponseObject>? Recipes { get; set; }

    public UserResponseObject(UserModel user, bool includeRecipes = false)
    {
        Id = user.Id;
        Username = user.Username;
        Email = user.Email;
        Role = user.Role;
        //Recipes = includeRecipes ? user.Recipes.Select(x => new RecipeResponseObject(x, false)).ToList() : default;
    }
}