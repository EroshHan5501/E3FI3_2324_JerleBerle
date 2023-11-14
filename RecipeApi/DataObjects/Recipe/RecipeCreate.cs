using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DataObjects.Recipe;

public class RecipeCreate
{
    [StringLength(100)] 
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }    

    public List<int> IngredientIds { get; set; } = new List<int>();
}
