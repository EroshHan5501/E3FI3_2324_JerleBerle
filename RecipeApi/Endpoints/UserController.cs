using Microsoft.AspNetCore.Mvc;

using RecipeApi.Database;
using RecipeApi.Database.Entities;
using RecipeApi.Parameters;

namespace RecipeApi.Endpoints
{
    public class UserController : RecipeBaseController<User, UserParameter>
    {
        public UserController(RecipeDbContext dbContext) 
            : base(dbContext) { }

        [HttpGet]
        public override async Task<IActionResult> Get(UserParameter parameter)
        {
            IEnumerable<Func<User, bool>> filters = parameter.ParseTo();

            IQueryable<User> query = DbContext.Users.AsQueryable();

            foreach (Func<User, bool> filter in filters)
            {
                query = (IQueryable<User>)query.Where(filter);
            }

            return Ok(query);
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
