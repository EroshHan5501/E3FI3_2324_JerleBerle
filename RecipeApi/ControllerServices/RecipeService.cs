//using RecipeApi.Database;
//using RecipeApi.Database.Entities;
//using RecipeApi.Responses;

//namespace RecipeApi.ControllerServices
//{
//    public class RecipeService : IControllerService<Recipe, Recipe, Recipe>
//    {
//        public IDbContext DbContext { get; }

//        public RecipeService(IDbContext dbContext)
//        {
//            DbContext = dbContext;
//        }

//        public Task<PagedEntityResponse<Recipe>> ReadAsync()
//        {
//            DbContext.GetEntitiesAsync<Recipe>("SELECT * FROM recipe ");
//        }

//        public Task CreateAsync(Recipe resource)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateAsync(Recipe resource)
//        {
//            throw new NotImplementedException();
//        }

//        public Task DeleteAsync(int resourceId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
