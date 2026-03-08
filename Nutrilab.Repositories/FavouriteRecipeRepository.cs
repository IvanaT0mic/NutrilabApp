using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.FavouriteRecipes;

namespace Nutrilab.Repositories
{
    public interface IFavouriteRecipeRepository
    {
        Task DeleteRangeAsync(List<FavouriteRecipe> data);
        Task<List<FavouriteRecipe>> GetByUserIdAsync(long userId);
        Task<FavouriteRecipe?> GetByIdAsync(long recipeId, long userId);
        Task<FavouriteRecipe> InsertAsync(FavouriteRecipe favouriteRecipe);
        Task DeleteAsync(FavouriteRecipe data);
    }

    public sealed class FavouriteRecipeRepository(EntityContext context) : BaseRepository<FavouriteRecipe>(context), IFavouriteRecipeRepository
    {
        public Task<FavouriteRecipe?> GetByIdAsync(long recipeId, long userId)
        {
            return GetQueryable()
                .Where(x => x.UserId == userId && x.RecipeId == recipeId)
                .FirstOrDefaultAsync();
        }

        public Task<List<FavouriteRecipe>> GetByUserIdAsync(long userId)
        {
            return GetQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
