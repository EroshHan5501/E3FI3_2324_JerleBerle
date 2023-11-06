using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public List<Recipe> Recipes { get; set; }
}
