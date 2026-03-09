using Nutrilab.DataAccess.Models.ShoppingListItems;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityModels;
using System.Collections.Generic;

namespace Nutrilab.DataAccess.Models.ShoppingLists
{
    public class ShoppingList : IShoppingList
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }

        public List<ShoppingListItem> Items { get; set; } = new();

        IUser IShoppingList.CreatedByUser => CreatedByUser;

        List<IShoppingListItem> IShoppingList.Items => Items.ToList<IShoppingListItem>();

    }
}
