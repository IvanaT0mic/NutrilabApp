using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Recipes;

namespace Nutrilab.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(long id);
        Task<Recipe> InsertAsync(Recipe data);
        Task<Recipe> UpdateAsync(Recipe data);
        Task DeleteAsync(Recipe data);
    }

    public sealed class RecipeRepository(EntityContext context)
        : BaseRepository<Recipe>(context), IRecipeRepository
    {
        public Task<List<Recipe>> GetAllAsync() =>
            GetQueryable().ToListAsync();

        public Task<Recipe?> GetByIdAsync(long id) =>
            GetQueryable()
                .Include(x => x.RecipeIngredients)
                    .ThenInclude(x => x.Ingredient)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
