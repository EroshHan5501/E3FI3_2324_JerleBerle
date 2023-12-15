using RecipeClient.Persistence;
using RecipeClient.ViewModels.User;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;

namespace RecipeClient.Commands;

internal class RegisterCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public RegisterViewModel Model { get; set; }    

    public RegisterCommand(RegisterViewModel model)
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

        HttpRequestMessage message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create<RegisterViewModel>(Model)
        };

        HttpResponseMessage response = client.Send(message);

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<string> cookies = response.Content.Headers.GetValues("cookies");

            CookieStore.FromCookies(cookies);
        }
    }
}
