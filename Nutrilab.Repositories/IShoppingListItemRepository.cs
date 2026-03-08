using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.ShoppingListItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Repositories
{
    public interface IShoppingListItemRepository
    {
        Task<ShoppingListItem?> GetByIdAsync(long id);
        Task<ShoppingListItem> InsertAsync(ShoppingListItem data);
        Task<ShoppingListItem> UpdateAsync(ShoppingListItem data);
        Task DeleteAsync(ShoppingListItem data);
    }

    public sealed class ShoppingListItemRepository(EntityContext context)
        : BaseRepository<ShoppingListItem>(context), IShoppingListItemRepository
    {
        public Task<ShoppingListItem?> GetByIdAsync(long id)
        {
            return GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
