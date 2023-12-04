using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Parameters;

public class ParameterBase
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }  
}
