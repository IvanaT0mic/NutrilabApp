using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos
{
    public class ShoppingListDetailOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ShoppingListItemOutgoingDto> Items { get; set; }
    }
}
