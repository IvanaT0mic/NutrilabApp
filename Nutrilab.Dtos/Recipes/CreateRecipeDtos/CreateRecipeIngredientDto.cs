namespace Nutrilab.Dtos.Recipes.CreateRecipeDtos
{
    public record CreateRecipeIngredientDto
    {
        public long IngredientId { get; init; }
        public decimal Quantity { get; init; }
    }
}
