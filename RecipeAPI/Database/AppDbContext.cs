using Microsoft.EntityFrameworkCore;

namespace RecipeAPI.Database
{
    public class AppDbContext : DbContext
    {
        string connectionString = "Server=localhost; User=root; Password=Gatling762; Database=recipeapp";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
