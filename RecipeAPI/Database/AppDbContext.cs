using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RecipeModel> Recipes { get; set; } = null!;
        public DbSet<IngredientModel> Ingredients { get; set; } = null!;
        public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; } = null!;
        public DbSet<RiuRelModel> RiuRels { get; set; } = null!;
        public DbSet<UserModel> Users { get; set; } = null!;
    }
}
