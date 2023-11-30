using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Database.Models;

[Table("UnitOfMeasurement")]
public class UnitOfMeasurementModel
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    //public List<RiuRelModel> Relations { get; set; }
}
