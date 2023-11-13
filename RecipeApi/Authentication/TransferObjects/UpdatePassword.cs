using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Authentication.TransferObjects;

public class UpdatePassword
{
    [Required]
    [MinLength(12, ErrorMessage = "Password must be min. 12 characters long")]
    public string OldPassword { get; set; } = null!;

    [Required]
    [MinLength(12, ErrorMessage = "Password must be min. 12 characters long")]
    public string NewPassword { get; set; } = null!;

    [Required]
    [MinLength(12, ErrorMessage = "Password must be min. 12 characters long")]
    public string ConfirmNew { get; set; } = null!;
}
