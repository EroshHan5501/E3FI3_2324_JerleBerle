﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi.Database.Entities;

[Table("unit")]
public class Unit : IKeyEntity
{
    [Key]
    [Column("measureId")]
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
