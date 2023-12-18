using RecipeClient.Commands;
using RecipeClient.ViewModels.Core;
using System.ComponentModel;
using System.Windows.Input;

namespace RecipeClient.ViewModels.User;

internal class LoginViewModel : ViewModelBase
{
    private string _email;
    private string _password;

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

    public ICommand LoginCommand => new LoginCommand(this);
}
