using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database.Models;

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

        public DbSet<RecipeModel> Recipes { get; set; }
        public DbSet<IngredientModel> Ingredients { get; set; }
        public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; }
        public DbSet<RiuRelModel> RiuRels { get; set; }
    }
}
