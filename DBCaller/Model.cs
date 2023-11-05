using Microsoft.EntityFrameworkCore;
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

        public DbSet<recipe> Recipees { get; set; }
        public DbSet<ingredient> Ingredients { get; set; }

        public string DbPath { get; }

        public RecipeContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "recipe.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
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
