using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos
{
    public class ShoppingListDetailOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ShoppingListItemOutgoingDto> Items { get; set; }
    }
}
