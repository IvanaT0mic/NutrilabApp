using Nutrilab.DataAccess.Models.ShoppingListItems;
using Nutrilab.DataAccess.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
