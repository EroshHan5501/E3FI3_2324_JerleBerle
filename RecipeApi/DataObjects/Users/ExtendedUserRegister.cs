using RecipeApi.Database.Entities;

namespace RecipeApi.DataObjects.Users;

public class ExtendedUserRegister : UserRegister
{
    public Role Role { get; set; }
}
