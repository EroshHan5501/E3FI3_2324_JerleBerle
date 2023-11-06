using Microsoft.EntityFrameworkCore;

using RecipeApi.Database.Entities;

namespace RecipeApi.Database;

public class RecipeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }  

    public DbSet<MeasureUnit> MeasureUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;port=3306;database=recipeappdb;user=vector;password=K/]zjUT)({?Xbdy?<+YEpsNzB38,*0$rc7DiAqvL",
            new MariaDbServerVersion(new Version(11, 1, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Recipes)
            .WithOne(x => x.User);

        modelBuilder.Entity<Recipe>()
            .HasMany(x => x.Ingredients)
            .WithMany(x => x.Recipes);

        modelBuilder.Entity<Ingredient>()
            .HasMany(x => x.MeasureUnits)
            .WithMany(x => x.Ingredients);

    }

}
