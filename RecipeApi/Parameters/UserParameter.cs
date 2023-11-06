using RecipeApi.Database.Entities;

namespace RecipeApi.Parameters
{
    public class UserParameter : ParameterBase<User>
    {
        protected override Func<User, bool> ParseToInternal(string key, string value) => key switch
        {
            "username" => (User user) => user.Username.Contains(value, StringComparison.OrdinalIgnoreCase),
            "email" => (User user) => user.Email.Contains(value, StringComparison.OrdinalIgnoreCase),
            "role" => (User user) => user.Role == (Role)int.Parse(value)
        };
    }
}
