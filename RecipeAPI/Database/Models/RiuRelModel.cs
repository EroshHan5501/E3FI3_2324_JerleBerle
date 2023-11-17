namespace RecipeAPI.Database.Models
{
    public class RiuRelModel
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public int UOMId { get; set; }
        public RecipeModel Recipe { get; set; }
        public IngredientModel Ingredient { get; set; }
        public UnitOfMeasurementModel UnitOfMeasurement { get; set; }
    }
}
