using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.Authentication.DataObjects;

public class Credentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}