using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeApi.DataObjects.Users;

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
    [JsonPropertyName("confirm")]
    public string ConfirmNew { get; set; } = null!;

    public bool IsConfirmed() => NewPassword == ConfirmNew;
}
