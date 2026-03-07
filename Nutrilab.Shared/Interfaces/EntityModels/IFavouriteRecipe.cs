namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IFavouriteRecipe
    {
        long UserId { get; }

        IUser User { get; }

        long RecipeId { get; }

        IRecipe Recipe { get; }

        DateTime CreatedAt { get; }
    }
}
