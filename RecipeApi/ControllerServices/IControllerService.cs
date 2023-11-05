using Microsoft.AspNetCore.Mvc;

using RecipeApi.Responses;

namespace RecipeApi.ControllerServices
{
    public interface IControllerService<TReturn, TCreate, TUpdate>
    {
        public Task<PagedEntityResponse<TReturn>> ReadAsync();

        public Task CreateAsync(TCreate resource);

        public Task UpdateAsync(TUpdate resource);

        public Task DeleteAsync(int resourceId);
    }
}
