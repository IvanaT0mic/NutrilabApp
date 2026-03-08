using Nutrilab.DataAccess.Models.FavouriteRecipes;
using Nutrilab.Repositories;
using Nutrilab.Services.Handlers;
using Nutrilab.Shared.Models.Exceptions;

namespace Nutrilab.Services
{
    public interface IFavouriteRecipeService
    {
        Task MarkAsFavouriteAsync(long id);
        Task RemoveFromFavouritesAsync(long id);
    }

    public sealed class FavouriteRecipeService(
        IFavouriteRecipeRepository favouriteRecipeRepository,
        ICurrentUserService currentUserService
        ) : IFavouriteRecipeService
    {
        public async Task MarkAsFavouriteAsync(long id)
        {
            var currentUser = currentUserService.GetCurrentUser();
            var recipe = await favouriteRecipeRepository.GetByIdAsync(id, currentUser.Id);
            if (recipe != null)
            {
                throw new NotFoundException($"Already marked as favourite");
            }

            var db = new FavouriteRecipe()
            {
                RecipeId = id,
                UserId = currentUser.Id,
            };

            await favouriteRecipeRepository.InsertAsync(db);
        }

        public async Task RemoveFromFavouritesAsync(long id)
        {
            var currentUser = currentUserService.GetCurrentUser();
            var recipe = await favouriteRecipeRepository.GetByIdAsync(id, currentUser.Id);
            if (recipe == null)
            {
                throw new NotFoundException($"Not favourite");
            }

            await favouriteRecipeRepository.DeleteAsync(recipe);
        }
    }
}
