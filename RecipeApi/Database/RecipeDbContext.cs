using Microsoft.EntityFrameworkCore;

using RecipeApi.Database.Entities;
using RecipeCommon.Secrets;
using System.Reflection;

namespace RecipeApi.Database;

public class RecipeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }  

    public DbSet<MeasureUnit> MeasureUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? basedir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);  

        SecretsFile secrets = SecretsFile.GetFrom("secrets.json", basedir);

        optionsBuilder.UseMySql(
            secrets.ConnectionString,
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
