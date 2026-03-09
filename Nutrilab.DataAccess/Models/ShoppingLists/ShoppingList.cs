using Nutrilab.DataAccess.Models.ShoppingListItems;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.ShoppingLists
{
    public class ShoppingList : IShoppingList
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CreatedByUserId { get; set; }

        public IUser CreatedByUser { get; set; }

        public List<IShoppingListItem> Items { get; set; } = new();
    }
}
