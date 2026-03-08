using Nutrilab.DataAccess.Models.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.DataAccess.Models.ShoppingListItems
{
    public class ShoppingListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }

        public bool IsChecked { get; set; }

        public long ShoppingListId { get; set; }

        public ShoppingList ShoppingList { get; set; }
    }
}

