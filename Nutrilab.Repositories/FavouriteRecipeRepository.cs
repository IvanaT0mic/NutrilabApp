using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.FavouriteRecipes;

namespace Nutrilab.Repositories
{
    public interface IFavouriteRecipeRepository
    {
        Task DeleteRangeAsync(List<FavouriteRecipe> data);
        Task<List<FavouriteRecipe>> GetByUserIdAsync(long userId);
    }

    public sealed class FavouriteRecipeRepository(EntityContext context) : BaseRepository<FavouriteRecipe>(context), IFavouriteRecipeRepository
    {
        public Task<List<FavouriteRecipe>> GetByUserIdAsync(long userId)
        {
            return GetQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
