using Nutrilab.Shared.Enums;
using Nutrilab.Shared.Interfaces.EntityAudit;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Interfaces.ServiceModels;

namespace Nutrilab.Services.RecipeServices.Models
{
    public sealed class RecipeDetailsServiceModel : IRecipeDetailsServiceModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public long CreatedByUserId { get; set; }

        public IUser CreatedByUser { get; set; }

        public int? PreparationTimeMinutes { get; set; }

        public MealCategory? MealCategory { get; set; }

        public DifficultyLvl? DifficultyLvl { get; set; }

        public bool IsFavourite { get; set; }

        public List<IRecipeIngredient> RecipeIngredients { get; set; }

        public List<IFavouriteRecipe> FavouriteUsers { get; set; }

        public List<IRecipeResource> Resources { get; set; }
    }
}
