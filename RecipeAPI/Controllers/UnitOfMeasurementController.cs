using Microsoft.AspNetCore.Mvc;

using RecipeAPI.Database;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Controllers;

public class UnitOfMeasurementController : BaseController
{    
    public UnitOfMeasurementController(AppDbContext context) : base(context)
    {
    }

    [HttpGet]
    public IEnumerable<UnitOfMeasurementModel> Get()
    {
        return DbContext.UnitsOfMeasurement.ToList();
    }
}
