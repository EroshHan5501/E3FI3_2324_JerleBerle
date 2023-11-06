using RecipeApi.Database.Entities;

namespace RecipeApi.Parameters;

public class RecipeParameter : ParameterBase<Recipe>
{
    private IEnumerable<string> GetIngredientsFiltersFromParameter(string value)
    {
        return value.Split(",");
    }

    protected override Func<Recipe, bool> ParseToInternal(string key, string value) => key switch
    {
        "title" => (Recipe recipe) => recipe.Title.Contains(value, StringComparison.OrdinalIgnoreCase),
        "startedAt" => (Recipe recipe) => recipe.StartedAt == DateTime.Parse(value),
        "description" => (Recipe recipe) => recipe.Description.Contains(value, StringComparison.OrdinalIgnoreCase),
        "ingredients" => (Recipe recipe) => recipe.Ingredients.Any(ingred => GetIngredientsFiltersFromParameter(value).Contains(ingred.Name))
    };
}
