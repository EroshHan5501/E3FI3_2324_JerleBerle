using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Database
{
    public class AppDbContext : DbContext
    {
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     DBUser user = DBUserHandler.GetUserData();
        //     // //string connectionString = "Server=localhost; User=root; Password=Gatling762; Database=recipeapp";
        //     string connectionString = $"Server={user.Server}; User={user.Name}; Password={user.Password}; Database={user.Database}";
        //     Console.Write(connectionString);
        //     base.OnConfiguring(optionsBuilder);
        //     optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RecipeModel> Recipes { get; set; } = null!;
        public DbSet<IngredientModel> Ingredients { get; set; } = null!;
        public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; } = null!;
        public DbSet<RiuRelModel> RiuRels { get; set; } = null!;

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     modelBuilder.Entity<RiuRelModel>().HasOne(x => x.Recipe).WithMany(x => x.Relations).HasForeignKey(x => x.RecipeId);
        //     modelBuilder.Entity<RiuRelModel>().HasOne(x => x.Ingredient).WithMany(x => x.Relations).HasForeignKey(x => x.IngredientId);
        //     modelBuilder.Entity<RiuRelModel>().HasOne(x => x.UnitOfMeasurement).WithMany().HasForeignKey(x => x.UnitOfMeasurementId);

        //     modelBuilder.Entity<RecipeModel>().Navigation(x => x.Relations).AutoInclude();
        //     modelBuilder.Entity<IngredientModel>().Navigation(x => x.Relations).AutoInclude();
        //     //modelBuilder.Entity<UnitOfMeasurementModel>().Navigation(x => x.Relations).AutoInclude();

        //     //modelBuilder.Entity<RiuRelModel>().Navigation(x => x.Recipe).AutoInclude();
        //     //modelBuilder.Entity<RiuRelModel>().Navigation(x => x.Ingredient).AutoInclude();
        //     //modelBuilder.Entity<RiuRelModel>().Navigation(x => x.UnitOfMeasurement).AutoInclude();
        // }
    }
}
