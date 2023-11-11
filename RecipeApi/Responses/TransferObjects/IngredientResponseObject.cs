using RecipeApi.Database.Entities;

namespace RecipeApi.Responses.TransferObjects;

public class IngredientResponseObject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

    public string UnitOfMeasurement { get; set; } = null!;

    public IngredientResponseObject(Ingredient ingredient)
    {
        Id = ingredient.Id;
        Name = ingredient.Name;
        Amount = ingredient.Amount.AmountValue;
        UnitOfMeasurement = ingredient.Unit.Name;
    }
}
