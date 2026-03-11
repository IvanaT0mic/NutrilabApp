using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Recipes;

namespace Nutrilab.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllAsync();
        Task<List<Recipe>> GetAllFavsByUserIdAsync(long userId);
        Task<Recipe?> GetByIdExtendedAsync(long id);
        Task<bool> AnyWithUserIdAsync(long userId);
        Task<Recipe> InsertAsync(Recipe data);
        Task<Recipe> UpdateAsync(Recipe data);
        Task DeleteAsync(Recipe data);
    }

    public sealed class RecipeRepository(EntityContext context)
        : BaseRepository<Recipe>(context), IRecipeRepository
    {
        public Task<bool> AnyWithUserIdAsync(long userId)
        {
            return GetQueryable()
                .Where(x => x.CreatedByUserId == userId)
                .AnyAsync();
        }

        public Task<List<Recipe>> GetAllAsync()
        {
            return GetQueryable().ToListAsync();
        }

        public Task<List<Recipe>> GetAllFavsByUserIdAsync(long userId)
        {
            return GetQueryable()
                .Where(r => r.FavouriteUsers.Any(f => f.UserId == userId))
                .ToListAsync();
        }

        public Task<Recipe?> GetByIdExtendedAsync(long id)
        {
            return GetQueryable()
                .Include(x => x.RecipeIngredients)
                    .ThenInclude(x => x.Ingredient)
                .Include(x => x.Resources)
                .Include(x => x.FavouriteUsers)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
