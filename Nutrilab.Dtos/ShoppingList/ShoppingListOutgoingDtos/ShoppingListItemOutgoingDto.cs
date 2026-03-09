using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos
{
    public class ShoppingListItemOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public bool IsChecked { get; set; }
    }
}
