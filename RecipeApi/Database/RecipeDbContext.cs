using Microsoft.EntityFrameworkCore;

using RecipeApi.Database.Entities;
using RecipeCommon.Secrets;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecipeApi.Database;

// TODO: Implement resolve 

[Table("ingredientrecipe")]
public class IngredientRecipe
{
    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; }

    public int RecipeId { get; set; }   

    public Recipe Recipe { get; set; }
}

public class RecipeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }  

    public DbSet<Unit> Units { get; set; }

    public DbSet<Amount> Amounts { get; set; }

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
        modelBuilder.Entity<User>()
            .HasMany(x => x.Recipes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        modelBuilder.Entity<Unit>()
            .HasMany(x => x.Ingredients)
            .WithOne(x => x.Unit)
            .HasForeignKey(x => x.UnitId)
            .IsRequired();

        modelBuilder.Entity<Amount>() 
            .HasMany(x => x.Ingredients)
            .WithOne(x => x.Amount)
            .HasForeignKey(x => x.AmountId)
            .IsRequired();

        modelBuilder.Entity<IngredientRecipe>().HasKey(x => new { x.IngredientId, x.RecipeId });

        modelBuilder.Entity<IngredientRecipe>()
            .HasOne(x => x.Ingredient)
            .WithMany(x => x.Recipes)
            .HasForeignKey(x => x.IngredientId);

        modelBuilder.Entity<IngredientRecipe>()
            .HasOne(x => x.Recipe)
            .WithMany(x => x.Ingredients)
            .HasForeignKey(x => x.RecipeId);

        modelBuilder.Entity<User>().Navigation(x => x.Recipes).AutoInclude();
        modelBuilder.Entity<Recipe>().Navigation(x => x.Ingredients).AutoInclude();
        modelBuilder.Entity<Recipe>().Navigation(x => x.User).AutoInclude();
        modelBuilder.Entity<Ingredient>().Navigation(x => x.Unit).AutoInclude();
        modelBuilder.Entity<Ingredient>().Navigation(x => x.Amount).AutoInclude();
    }
}
