using RecipeClient.Commands;
using RecipeClient.ViewModels.Core;
using System.Windows.Input;

namespace RecipeClient.ViewModels.User;

internal class RegisterViewModel : ViewModelBase
{
    private string _username;
    private string _email;
    private string _password;

    public string Username
    {
        get {  return _username; }
        set
        {
            _username = value;
            OnPropertyChange(nameof(Username));
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChange(nameof(Email));
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChange(nameof(Password));
        }
    }

    public ICommand RegisterCommand => new RegisterCommand();
}
