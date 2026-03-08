using Nutrilab.DataAccess.Models.ShoppingListItems;
using Nutrilab.DataAccess.Models.Users;

namespace Nutrilab.DataAccess.Models.ShoppingLists
{
    public class ShoppingList
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }

        public List<ShoppingListItem> Items { get; set; } = new();
    }
}
