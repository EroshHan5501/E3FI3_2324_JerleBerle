using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi.Database.Entities;

[Table("amount")]
public class Amount : IKeyEntity
{
    [Key]
    [Column("amountId")]
    public int Id { get; set; }

    public int AmountValue { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public Amount()
    {
    
    }

    public Amount(int amountValue)
    {
        AmountValue = amountValue;
    }
}
