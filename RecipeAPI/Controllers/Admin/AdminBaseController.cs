using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RecipeAPI.Database;

namespace RecipeAPI.Controllers.Admin;

[Authorize(Roles = "Admin")]
public abstract class AdminBaseController : BaseController
{
    public AdminBaseController(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}