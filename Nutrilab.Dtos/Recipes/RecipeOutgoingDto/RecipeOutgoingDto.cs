using Nutrilab.Shared.Enums;

namespace Nutrilab.Dtos.Recipes.RecipeOutgoingDto
{
    public sealed class RecipeOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? PreparationTimeMinutes { get; set; }
        public MealCategory? MealCategory { get; set; }
        public DifficultyLvl? DifficultyLvl { get; set; }
    }
}
