using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.RecipeResources;

namespace Nutrilab.Repositories
{
    public interface IRecipeResourceRepository
    {
        Task<RecipeResource?> GetRecipeResourceAsync(long id);
        Task<List<RecipeResource>> GetByRecipeIdAsync(long id);
        Task<RecipeResource> InsertAsync(RecipeResource recipeResource);
        Task DeleteAsync(RecipeResource data);
        Task DeleteRangeAsync(List<RecipeResource> data);
    }

    public sealed class RecipeResourceRepository(EntityContext context)
        : BaseRepository<RecipeResource>(context), IRecipeResourceRepository
    {
        public Task<List<RecipeResource>> GetByRecipeIdAsync(long id)
        {
            return GetQueryable()
                .Where(x => x.RecipeId == id)
                .ToListAsync();
        }

        public Task<RecipeResource?> GetRecipeResourceAsync(long id)
        {
            return GetQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
