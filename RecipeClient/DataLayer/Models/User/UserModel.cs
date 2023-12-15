using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace RecipeClient.DataLayer.Models.User;

internal enum Role
{
    User = 0,
    Admin = 1
}

internal struct ApiRoutes
{
    public Uri ReadUri { get; set; }

    public Uri CreateUri { get; set; }

    public Uri UpdateUri { get; set; }

    public Uri DeleteUri { get; set; }
}

internal interface IApiPayload
{
    public ApiRoutes Routes { get; set; }

    public HttpContent Create();

    public HttpContent Update();

}

internal struct UserModel : IApiPayload
{
    public string Email { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }

    [JsonIgnore]
    public ApiRoutes Routes { get; set; }

    public UserModel(
        string email, 
        string username, 
        Role role,
        ApiRoutes routes)
    {
        Email = email;
        Username = username;
        Role = role;
        Routes = routes;
    }

    public HttpContent Create()
    {
        return JsonContent.Create<UserModel<T>>(this);
    }

    public HttpContent Update()
    {
        throw new NotImplementedException();
    }
}
