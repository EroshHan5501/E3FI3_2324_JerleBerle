
using RecipeClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeClient;

public enum Routes
{
    Login,
    Register,
    ListRecipes,
    DetailRecipe
}

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        public MainWindow()
        {
            InitializeComponent();
            SwitchToView("register.xaml");
        }

         private void SwitchToView(string viewFileName)
         {
            var viewUri = new Uri(viewFileName, UriKind.Relative);
            var view = (UserControl)LoadComponent(viewUri);
            contentControl.Content = view;
        }
    }
}
