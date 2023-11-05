namespace RecipeApi.Responses
{
    public class PagedEntityResponse<TEntity>
    {
        public int PageSize { get; }

        public int CurrentPage { get; }

        public int TotalPages { get; }

        public IEnumerable<TEntity> Entities { get; }

        public PagedEntityResponse(IEnumerable<TEntity> entities)
        {
            Entities = entities;
        }
    }
}
