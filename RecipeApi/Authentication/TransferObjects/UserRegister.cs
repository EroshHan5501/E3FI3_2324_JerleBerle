using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Authentication.TransferObjects;

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
