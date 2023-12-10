using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeAPI.DataObjects.Users;

public class UserUpdate
{
    [Required]
    [JsonPropertyName("userId")]
    public int Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Username can't have more then 50 characters!")]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Please provide a valid email!")]
    public string Email { get; set; } = null!;
}