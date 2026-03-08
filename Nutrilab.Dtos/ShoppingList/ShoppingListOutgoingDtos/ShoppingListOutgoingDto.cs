using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos
{
    public class ShoppingListOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ItemCount { get; set; }
    }
}
