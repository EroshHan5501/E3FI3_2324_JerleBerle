using RecipeClient.Persistence;
using RecipeClient.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;

namespace RecipeClient.Commands;

internal class LoginCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public LoginViewModel Model { get; set; }

    public LoginCommand(LoginViewModel model)
    {
        Model = model;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        using HttpClient client = new HttpClient();

        HttpRequestMessage request = new()
        {
            Content = JsonContent.Create<LoginViewModel>(Model),
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://localhost:8068/api/login/")
        };

        HttpResponseMessage response = client.Send(request);

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<string> cookieValues = response.Headers.GetValues("cookies");

            CookieStore.FromCookies(cookieValues);
        }
    }
}
