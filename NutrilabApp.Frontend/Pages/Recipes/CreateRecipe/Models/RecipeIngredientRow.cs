namespace NutrilabApp.Frontend.Pages.Recipes.CreateRecipe.Models
{

    public class RecipeIngredientRow
    {
        public long IngredientId { get; set; }
        public string IngredientName { get; set; } = "";
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "";
    }
}
