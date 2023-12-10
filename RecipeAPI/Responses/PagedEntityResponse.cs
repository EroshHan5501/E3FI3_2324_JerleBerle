using System.Text.Json.Serialization;

namespace RecipeAPI.Responses;

public class PagedEntityResponse<TEntity>
{
    public int PageSize { get; }

    public int CurrentPage { get; }

    public int TotalPages { get; }

    [JsonPropertyName("content")]
    public List<TEntity> Entities { get; }

    public PagedEntityResponse(
        int pageSize,
        int currentPage,
        int totalPages,
        List<TEntity> entities)
    {
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        Entities = entities;
    }
}