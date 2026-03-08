namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IUser
    {
        long Id { get; }

        string Email { get; }

        string PasswordHash { get; }

        List<IUserRole> UserRoles { get; }

        List<IRecipe> Recipes { get; }

        List<IFavouriteRecipe> FavouriteRecipes { get; }
    }
}
