namespace Nutrilab.Dtos.Recipes.CreateRecipeDtos
{
    public record PatchRecipeDto
    {
        public List<RecipeIngredientDto> RecipeIngredients { get; init; }
    }
}
