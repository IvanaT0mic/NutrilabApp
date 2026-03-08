using Nutrilab.Shared.Enums;

namespace Nutrilab.Dtos.Recipes.UpdateRecipeDtos
{
    public record UpdateRecipeDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public int? PreparationTimeMinutes { get; init; }
        public MealCategory? MealCategory { get; init; }
        public DifficultyLvl? DifficultyLvl { get; init; }
    }
}
