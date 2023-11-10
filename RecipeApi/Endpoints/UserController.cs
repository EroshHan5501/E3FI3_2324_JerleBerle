using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

using System.Linq.Expressions;

namespace RecipeApi.Endpoints
{
    public class UserController : RecipeBaseController<User, UserParameter>
    {
        public UserController(RecipeDbContext dbContext) 
            : base(dbContext) { }

        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery]UserParameter parameter)
        {
            IEnumerable<Expression<Func<User, bool>>> filters = parameter.ParseTo();

            IQueryable<User> query = DbContext.Users;

            foreach (Expression<Func<User, bool>> filter in filters)
            {
                query = query.Where(filter);
            }

            return Ok(query.Include(x => x.Recipes));
        }

        [HttpPost("create")]
        public override async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPost("update")]
        public override async Task<IActionResult> Update()
        {
            return Ok();
        }

        [HttpDelete("delete")]
        public override async Task<IActionResult> Delete()
        {

            return Ok();
        }
    }
}
