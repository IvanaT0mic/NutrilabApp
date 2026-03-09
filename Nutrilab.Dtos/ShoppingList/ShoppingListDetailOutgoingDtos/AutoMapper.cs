using AutoMapper;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IShoppingList, ShoppingListDetailOutgoingDto>();
            CreateMap<IShoppingListItem, ShoppingListItemOutgoingDto>();
        }
    }
}
