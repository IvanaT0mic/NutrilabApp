namespace Nutrilab.Dtos.Recipes.CreateRecipeDtos
{
    public record RecipeIngredientDto
    {
        public long IngredientId { get; init; }
        public decimal Quantity { get; init; }
    }
}
