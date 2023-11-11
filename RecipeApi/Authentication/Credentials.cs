using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Authentication;

public class Credentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
