using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database;

namespace RecipeAPI.Controllers.Admin;

[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin")]
public abstract class AdminBaseController : BaseController
{
    public AdminBaseController(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}