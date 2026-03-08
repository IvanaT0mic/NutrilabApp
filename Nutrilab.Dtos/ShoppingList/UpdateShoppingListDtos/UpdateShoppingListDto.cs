using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos
{
    public record UpdateShoppingListDto
    {
        public string Name { get; init; }
    }
}
