
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
        InitializeComponent();
    }

    public async Task StartupRouting()
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage result = await client.GetAsync("https://localhost:6085/api/Recipe/");

        IEnumerable<string> rest = new[] { "hello", "world" };

        if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            // TODO: Route to login view 
        }
        else
        {
            this.Content = new RecipeListView();
        }
    }
}
