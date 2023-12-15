using RecipeAPI.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.DataObjects.Users;

public class UpdateRole
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public Role Role { get; set; }
}
