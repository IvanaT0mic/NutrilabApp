using Nutrilab.Shared.Enums;

namespace Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos
{
    public sealed class RecipeDetailOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageBase64 { get; set; }
        public int? PreparationTimeMinutes { get; set; }
        public MealCategory? MealCategory { get; set; }
        public DifficultyLvl? DifficultyLvl { get; set; }
        public bool IsFavourite { get; set; }
        public List<RecipeIngredientOutgoingDto> Ingredients { get; set; }
    }
}
