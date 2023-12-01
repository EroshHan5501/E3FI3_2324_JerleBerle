using Microsoft.EntityFrameworkCore;
using RecipeAPI.Responses;

namespace RecipeAPI.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PagedEntityResponse<TResult>> ToPageAsync<TResult>(this IQueryable<TResult> entities, int pageIndex, int pageSize)
    {
        int count = await entities.CountAsync();
        List<TResult> items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedEntityResponse<TResult>(
            pageSize,
            pageIndex,
            count / pageSize,
            items);
    }
}