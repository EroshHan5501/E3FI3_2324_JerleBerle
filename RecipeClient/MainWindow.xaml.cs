using RecipeClient.Auth;
using RecipeClient.View;

using System.Windows;

namespace RecipeClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RootGrid.Children.Add(new MainView());
        }

    }
}
