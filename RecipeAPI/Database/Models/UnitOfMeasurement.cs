using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.Database.Models;

public class UnitOfMeasurement
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
}
