using RecipeClient.Database.Core;
using RecipeClient.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeClient.Database.InMemory
{
    internal class InMemRecipeRepository : IRepository<Recipe>
    {
        private List<Recipe> Recipes { get; } = new List<Recipe>();

        public void Create(Recipe entity)
        {
            Recipes.Add(entity);
        }

        public void Delete(int id)
        {
            Recipe recipe = GetSingle(recipe => recipe.Id == id);

            Recipes.Remove(recipe); 
        }

        public IEnumerable<Recipe> Get(Func<Recipe, bool> filter)
        {
            return Recipes.Where(filter);
        }

        public Recipe GetSingle(Func<Recipe, bool> filter)
        {
            Recipe? recipe = Recipes.SingleOrDefault(filter);

            if (recipe is null)
            {
                throw new Exception(
                    "Recipe does not exists");
            }

            return recipe;
        }

        public void Update(Recipe entity)
        {
            Recipe recipe = GetSingle(x => x.Id == entity.Id);

            int index = Recipes.IndexOf(recipe);

            if (index == -1)
            {
                throw new Exception(
                    "Recipe does not exists!");
            }

            Recipes[index] = entity;
        }
    }
}
