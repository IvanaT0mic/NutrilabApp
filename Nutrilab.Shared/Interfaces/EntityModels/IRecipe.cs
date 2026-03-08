using Nutrilab.Shared.Enums;
using Nutrilab.Shared.Interfaces.EntityAudit;

namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IRecipe
    {
        long Id { get; }

        string Name { get; }

        string? Description { get; }

        long CreatedByUserId { get; }

        IUser CreatedByUser { get; }

        int? PreparationTimeMinutes { get; }

        MealCategory? MealCategory { get; }

        DifficultyLvl? DifficultyLvl { get; }

        List<IRecipeIngredient> RecipeIngredients { get; }

        List<IFavouriteRecipe> FavouriteUsers { get; }

        List<IRecipeResource> Resources { get; }
    }
}
