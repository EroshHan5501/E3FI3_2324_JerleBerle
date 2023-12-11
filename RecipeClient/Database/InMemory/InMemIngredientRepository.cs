using RecipeClient.Database.Core;
using RecipeClient.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeClient.Database.InMemory
{
    // I think we can model this better to use only a single class with a generic type 
    internal class InMemIngredientRepository : IRepository<Ingredient>
    {
        public List<Ingredient> Ingredients { get; } = new List<Ingredient>();

        public void Create(Ingredient entity)
        {
            Ingredients.Add(entity);
        }

        public void Delete(int id)
        {
            Ingredient ingredient = GetSingle(x => x.Id == id);
            Ingredients.Remove(ingredient);
        }

        public IEnumerable<Ingredient> Get(Func<Ingredient, bool> filter)
        {
            return Ingredients.Where(filter);
        }

        public Ingredient GetSingle(Func<Ingredient, bool> filter)
        {
            Ingredient? ingredient = Ingredients.FirstOrDefault(filter);

            if (ingredient is null)
            {
                throw new Exception(
                    "Ingredient does not exists!");
            }

            return ingredient;
        }

        public void Update(Ingredient entity)
        {
            Ingredient ingredient = GetSingle(x => x.Id == entity.Id);

            int index = Ingredients.IndexOf(ingredient);

            Ingredients[index] = entity;
        }
    }
}
