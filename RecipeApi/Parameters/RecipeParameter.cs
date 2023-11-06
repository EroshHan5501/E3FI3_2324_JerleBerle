using RecipeApi.Database.Entities;

namespace RecipeApi.Parameters;

public class RecipeParameter : ParameterBase<Recipe>
{
    protected override Func<Recipe, bool> ParseToInternal(string key, string value) => key switch
    {
        "title" => (Recipe recipe) => recipe.Title.Contains(value, StringComparison.OrdinalIgnoreCase),
        "startedAt" => (Recipe recipe) => recipe.StartedAt == DateTime.Parse(value),
        "description" => (Recipe recipe) => recipe.Description.Contains(value, StringComparison.OrdinalIgnoreCase),
    };
}
