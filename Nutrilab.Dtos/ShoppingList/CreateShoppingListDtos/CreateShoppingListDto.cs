using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos
{
    public record CreateShoppingListDto
    {
        public string Name { get; init; }
        public List<CreateShoppingListItemDto> Items { get; init; } = new();
    }
}
