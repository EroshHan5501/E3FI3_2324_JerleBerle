using RecipeApi.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace RecipeApi.Database.Entities;

public enum Role
{
    User = 0,
    Admin = 1,
}

[Table("user")]
public class User : IKeyEntity
{
    [Key]
    [Column("userId")]
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public List<Recipe> Recipes { get; set; } =  new List<Recipe>();

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = HashHelper.GenerateSHA512Hash(password);
        Role = Role.User;
    }

    public static Dictionary<Role, string> RoleNameMapping = new Dictionary<Role, string>()
    {
        { Role.Admin, "Admin" },
        { Role.User, "User" }
    };

    public ClaimsPrincipal GeneratePrincipal()
    {
        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, Id.ToString()),
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role, RoleNameMapping[Role])
            }));
    }
}
