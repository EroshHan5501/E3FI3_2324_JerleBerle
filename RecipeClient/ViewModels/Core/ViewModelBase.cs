using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecipeClient.ViewModels.Core;

internal class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChange([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }  
}
