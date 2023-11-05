using System.Data.Common;

namespace RecipeApi.Database.Entities
{
    public class Recipe : IDatabaseReadable<Recipe>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public int UserId { get; set; }

        public static Recipe CreateFrom(DbDataReader reader)
        {
            Recipe recipe = new Recipe();

            recipe.Id = reader.GetInt32(0);
            recipe.Title = reader.GetString(1);
            recipe.CreatedAt = reader.GetDateTime(2);
            recipe.Description = reader.GetString(3);
            recipe.ImageUrl = reader.GetString(4);
            recipe.UserId = reader.GetInt32(5);

            return recipe;
        }
    }
}
