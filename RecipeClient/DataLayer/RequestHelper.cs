using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecipeClient.Model;

// Trennen zwischen HttpGet und HttpPost 

internal class HttpContext
{
    public HttpRequestMessage Payload { get; }

    public HttpContext(HttpRequestMessage payload)
    {
        Payload = payload;
    }
   
    public async Task<TEntity> Get<TEntity>()
    {
        using HttpClient client = new HttpClient();

        await client.GetAsync()
    }
}
