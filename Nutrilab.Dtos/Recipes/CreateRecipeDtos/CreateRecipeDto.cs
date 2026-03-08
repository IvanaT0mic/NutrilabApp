namespace Nutrilab.Dtos.Recipes.CreateRecipeDtos
{
    public record CreateRecipeDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<CreateRecipeIngredientDto> Ingredients { get; init; }
    }
}
