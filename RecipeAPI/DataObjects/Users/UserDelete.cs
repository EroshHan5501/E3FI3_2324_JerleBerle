﻿using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.DataObjects.Users;

public class UserDelete
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}