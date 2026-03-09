using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IShoppingListItem
    {
        long Id { get; }

        string Name { get; }

        decimal Quantity { get; }

        string Unit { get; }

        bool IsChecked { get; }

        long ShoppingListId { get; }

        IShoppingList ShoppingList { get; }
    }
}
