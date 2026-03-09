using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.ShoppingLists;

namespace Nutrilab.Repositories
{
    public interface IShoppingListRepository
    {
        Task<List<ShoppingList>> GetAllByUserIdAsync(long userId);
        Task<ShoppingList?> GetByIdWithItemsAsync(long id);
        Task<ShoppingList> InsertAsync(ShoppingList data);
        Task<ShoppingList> UpdateAsync(ShoppingList data);
        Task DeleteAsync(ShoppingList data);
    }

    public sealed class ShoppingListRepository(EntityContext context)
        : BaseRepository<ShoppingList>(context), IShoppingListRepository
    {
        public Task<List<ShoppingList>> GetAllByUserIdAsync(long userId)
        {
            return GetQueryable()
                .Include(x => x.Items)
                .Where(x => x.CreatedByUserId == userId)
                .ToListAsync();
        }

        public Task<ShoppingList?> GetByIdWithItemsAsync(long id)
        {
            return GetQueryable()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
