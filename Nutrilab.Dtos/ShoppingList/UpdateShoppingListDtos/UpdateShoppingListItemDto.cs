using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos
{
    public record UpdateShoppingListItemDto
    {
        public string Name { get; init; }
        public decimal Quantity { get; init; }
        public string Unit { get; init; }
        public bool IsChecked { get; init; }
    }
}
