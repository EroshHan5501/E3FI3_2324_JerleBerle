using RecipeApi.Database.Entities;

using System.Linq.Expressions;

namespace RecipeApi.Parameters;

public class IngredientParameter : ParameterBase<Ingredient>
{
    protected override Expression<Func<Ingredient, bool>> ParseToInternal(string key, string value)
    {
        return (ingredient) => true;
    }
}
