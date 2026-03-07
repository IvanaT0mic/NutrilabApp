namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IRecipe
    {
        long Id { get; }

        string Name { get; }

        string? Description { get; }

        long CreatedByUserId { get; }

        IUser CreatedByUser { get; }

        List<IRecipeIngredient> RecipeIngredients { get; }

        List<IFavouriteRecipe> FavouriteUsers { get; }
    }
}
