using RecipeAPI.Exceptions;
using System.Text.Json;

namespace RecipeAPI.Extensions;

internal static class HttpRequestExtensions
{
    public static async Task<TEntity> BodyAsJsonAsync<TEntity>(this HttpRequest request)
    {
        Stream body = request.Body;
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            TEntity? entity = await JsonSerializer
                .DeserializeAsync<TEntity>(body, options);

            if (entity == null)
            {
                throw HttpException.BadRequest("Could not deserialize json");
            }

            return entity;
        }
        catch (JsonException)
        {
            throw HttpException.BadRequest("Malformed request body!");
        }
    }
}