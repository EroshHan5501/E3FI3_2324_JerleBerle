using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Database.Models;

[Table("Ingredient")]
public class IngredientModel
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }
}
