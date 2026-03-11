using Nutrilab.Shared.Enums;

namespace NutrilabApp.Frontend.Services.RecipeServices.Models
{
    public class RecipeCardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
        public int? PreparationTimeMinutes { get; set; }
        public MealCategory? MealCategory { get; set; }
        public DifficultyLvl? DifficultyLvl { get; set; }
    }
}
