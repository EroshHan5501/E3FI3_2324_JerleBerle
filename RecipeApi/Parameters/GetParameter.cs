namespace RecipeApi.Parameters
{
    public abstract class GetParameter
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string SearchInput { get; set; }

        public abstract void TransformTo();
    }
}
