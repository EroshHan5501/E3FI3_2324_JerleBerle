namespace DataProviderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            WebApplication app = builder.Build();

            app.UseStaticFiles();
            app.MapDefaultControllerRoute();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}