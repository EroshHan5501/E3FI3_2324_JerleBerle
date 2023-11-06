using Microsoft.EntityFrameworkCore;

namespace DBCaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using (var context = new RecipeContext())
            {
                //var entities = context.recipe.Where(e => e.Name == "Blini").ToList();
                var entities = context.recipe.ToList();
                foreach (var entity in entities)
                {
                    Console.WriteLine($"ID: {entity.Id}, Name: {entity.Name}");
                }
            }
        }
    }
}