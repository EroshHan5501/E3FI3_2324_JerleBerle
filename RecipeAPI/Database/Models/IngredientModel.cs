using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Database.Models;

public class IngredientModel
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    public string Name { get; set; }
}
