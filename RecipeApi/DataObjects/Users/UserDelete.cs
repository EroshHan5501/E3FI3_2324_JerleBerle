using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DataObjects.Users;

public class UserDelete
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
