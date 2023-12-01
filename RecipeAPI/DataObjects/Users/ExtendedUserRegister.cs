using RecipeAPI.Database.Models;

namespace RecipeAPI.DataObjects.Users;

public class ExtendedUserRegister : UserRegister
{
    public Role Role { get; set; }
}