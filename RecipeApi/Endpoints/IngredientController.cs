using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Database.Extensions;
using RecipeApi.DataObjects.Ingredient;
using RecipeApi.Exceptions;
using RecipeApi.Parameters;
using RecipeApi.Responses;
using RecipeApi.Responses.TransferObjects;

using System.Linq.Expressions;

namespace RecipeApi.Endpoints;

public class IngredientController : RecipeBaseController<Ingredient, IngredientParameter, IngredientCreate, IngredientUpdate>
{
    public IngredientController(RecipeDbContext dbContext) : base(dbContext)
    {
    }

    [HttpGet]
    public override async Task<IActionResult> Get([FromQuery]IngredientParameter parameter)
    {
        IEnumerable<Expression<Func<Ingredient, bool>>> filters = parameter.ParseTo();

        IQueryable<Ingredient> query = DbContext.Ingredients;

        foreach (Expression<Func<Ingredient, bool>> filter in filters)
        {
            query = query.Where(filter);
        }

        PagedEntityResponse<IngredientResponseObject> result = await query
            .Select(ingred => new IngredientResponseObject(ingred))
            .ToPageAsync(parameter.PageIndex, parameter.PageSize);

        return Ok(result);  
    }

    [HttpPost("create")]
    public override async Task<IActionResult> Create(IngredientCreate body)
    {
        // We need to check if the ingredient exists!
        Ingredient? ingred = DbContext.Ingredients
            .FirstOrDefault(ingred =>
                ingred.Name == body.Name &&
                ingred.Amount.AmountValue == body.Amount &&
                ingred.Unit.Name == body.UnitOfMeasurement);

        if (ingred is not null)
        {
            throw HttpException.BadRequest(
                "This ingredient already exists!");
        }

        Unit unitOfMeasure;

        if (DbContext.Units.Any(unit => unit.Name == body.UnitOfMeasurement))
        {
            unitOfMeasure = DbContext.Units.FirstOrDefault(unit => unit.Name == body.UnitOfMeasurement)!;
        }
        else
        {
            Unit unit = new Unit(body.UnitOfMeasurement);
            unitOfMeasure = DbContext.Units.Add(unit).Entity;
        }

        Amount amount;

        if (DbContext.Amounts.Any(amount => amount.AmountValue == body.Amount))
        {
            amount = DbContext.Amounts.FirstOrDefault(amount => amount.AmountValue == body.Amount)!;
        }
        else
        {
            Amount newAmount = new Amount(body.Amount);
            amount = DbContext.Amounts.Add(newAmount).Entity;
        }

        Ingredient ingredient = new Ingredient(body.Name, unitOfMeasure, amount);

        DbContext.Ingredients.Add(ingredient);  

        await DbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("update")]
    public override async Task<IActionResult> Update(IngredientUpdate body)
    {
        return NotFound();
    }

    [HttpDelete("delete")]
    public override async Task<IActionResult> Delete()
    {
        throw new NotImplementedException();
    }

}
