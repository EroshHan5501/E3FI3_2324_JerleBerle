using RecipeApi.DataObjects.Users;
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

    public List<Recipe> Recipes { get; set; } = new List<Recipe>();

    public User(string username, string email, string password)
        : this(username, email, password, Role.User)
    {
        
    }

    public User(string username, string email, string password, Role role)
    {
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }

    public static Dictionary<Role, string> RoleNameMapping = new Dictionary<Role, string>()
    {
        { Role.Admin, "Admin" },
        { Role.User, "User" }
    };

    public void Update(UserUpdate data)
    {
        Username = data.Username;
        Email = data.Email;
    }

    public void UpdatePassword(string newPassword)
    {
        Password = HashHelper.GenerateSHA512Hash(newPassword); ;
    }

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
