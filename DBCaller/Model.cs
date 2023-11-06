using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DBCaller
{
    public class RecipeContext : DbContext
    {

        public DbSet<recipe> recipe { get; set; }
        public DbSet<ingredient> Ingredients { get; set; }

        public string DbPath { get; }
        private const string connectionString = "server=localhost;port=3306;database=recipeapp;user=root;password=Gatling762";
        ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

    public class recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UPM { get; set; }
    }
}
