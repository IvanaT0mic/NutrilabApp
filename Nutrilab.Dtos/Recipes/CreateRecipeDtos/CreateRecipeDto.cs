using Nutrilab.Shared.Enums;

namespace Nutrilab.Dtos.Recipes.CreateRecipeDtos
{
    public record CreateRecipeDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<RecipeIngredientDto> Ingredients { get; init; }
        public int? PreparationTimeMinutes { get; init; }
        public MealCategory? MealCategory { get; init; }
        public DifficultyLvl? DifficultyLvl { get; init; }
    }
}
