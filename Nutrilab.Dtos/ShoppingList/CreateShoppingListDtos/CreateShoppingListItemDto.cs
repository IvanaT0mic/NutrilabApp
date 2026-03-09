using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos
{
    public record CreateShoppingListItemDto
    {
        public string Name { get; init; }
        public decimal Quantity { get; init; }
        public string Unit { get; init; }
    }
}
