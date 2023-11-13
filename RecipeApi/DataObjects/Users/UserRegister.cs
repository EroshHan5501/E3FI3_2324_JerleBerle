using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DataObjects.Users;

public class UserRegister
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [MinLength(12)]
    public string Password { get; set; }
}
