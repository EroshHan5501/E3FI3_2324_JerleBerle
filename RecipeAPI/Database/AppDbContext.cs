using Microsoft.EntityFrameworkCore;
using RecipeAPI.Database.Models;

namespace RecipeAPI.Database
{
    public class AppDbContext : DbContext
    {
        string connectionString = "Server=localhost;User=vector;Password=K/]zjUT)({?Xbdy?<+YEpsNzB38,*0$rc7DiAqvL;Database=recipeapp";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<RecipeModel> Recipes { get; set; } = null!;

        public DbSet<IngredientModel> Ingredients { get; set; } = null!;

        public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; } = null!;

        public DbSet<RiuRelModel> RiuRels { get; set; } = null!;
        
        public DbSet<UserModel> Users { get; set; } = null!;

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
