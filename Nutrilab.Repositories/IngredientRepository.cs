using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Ingredients;

namespace Nutrilab.Repositories
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> GetAllAsync();
        Task<Ingredient?> GetByIdAsync(long id);
        Task<List<Ingredient>> GetAllByIdsAsync(List<long> ids);
        Task<Ingredient> InsertAsync(Ingredient data);
        Task<List<Ingredient>> InsertRangeAsync(List<Ingredient> data);
        Task DeleteAsync(Ingredient ingredient);
    }

    public sealed class IngredientRepository(EntityContext context) : BaseRepository<Ingredient>(context), IIngredientRepository
    {
        public Task<List<Ingredient>> GetAllAsync()
        {
            return GetQueryable().ToListAsync();
        }

        public Task<List<Ingredient>> GetAllByIdsAsync(List<long> ids)
        {
            return GetQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public Task<Ingredient?> GetByIdAsync(long id)
        {
            return GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
