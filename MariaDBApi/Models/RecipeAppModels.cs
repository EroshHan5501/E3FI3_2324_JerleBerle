using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariaDBApi.Models
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
        : base(options)
        {
        }

        public DbSet<Recipe> RecipeItems { get; set; } = null!;
        public DbSet<Ingredient> IngredientItems { get; set; } = null!;
    }

    [Table("Recipe")]
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("Ingredient")]
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UnitOfMeasurement
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RI_Rel
    {

    }
}
