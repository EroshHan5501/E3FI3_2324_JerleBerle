using RecipeApi.Exceptions;

using System.Linq.Expressions;

namespace RecipeApi.Parameters
{
    public abstract class ParameterBase<TEntity>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string? Parameters { get; set; }

        protected abstract Expression<Func<TEntity, bool>> ParseToInternal(string key, string value);

        public IEnumerable<Expression<Func<TEntity, bool>>> ParseTo()
        {
            Expression<Func<TEntity, bool>> defaultFilter = (TEntity entity) => true;

            if (Parameters is null)
            {
                yield return defaultFilter;
                yield break;
            }

            string[] parameters = Parameters.Split("&");
            int count = 0;

            foreach (string parameter in parameters)
            {
                count += 1;
                string normalized = parameter.Trim();
                if (normalized.Count(x => x == ':') != 1)
                {
                    continue;
                }

                string[] keyValue = parameter.Split(':');

                Expression<Func<TEntity, bool>> result; 
                try
                {
                    result = ParseToInternal(keyValue[0], keyValue[1]);
                }
                catch(Exception)
                {
                    throw HttpException.BadRequest(
                        "Malformed search parameters!");
                }

                yield return result;
            }

            if (count == 0)
            {
                yield return defaultFilter;
            }
        }
    }
}
