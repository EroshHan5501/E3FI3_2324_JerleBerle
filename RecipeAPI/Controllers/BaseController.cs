using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Database;
using RecipeAPI.Database.Models;
using RecipeAPI.Exceptions;
using System.Security.Claims;

namespace RecipeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public abstract class BaseController : ControllerBase
{
    public AppDbContext DbContext { get; set; }

    public UserModel CurrentUser
    {
        get
        {
            Claim? idClaim = this.HttpContext.User.FindFirst(ClaimTypes.Sid);

            if (idClaim is null)
            {
                throw HttpException.NotFound("Can't identify current user!");
            }

            int id = int.Parse(idClaim?.Value!);

            UserModel? currentUser = DbContext.Users
                .FirstOrDefault(u => u.Id == id);

            if (currentUser is null)
            {
                throw HttpException.NotFound("User does not exists!");
            }

            return currentUser;
        }
    }

    public BaseController(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
}