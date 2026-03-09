using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IShoppingList, ShoppingListOutgoingDto>()
                .ForMember(x => x.ItemCount, opt => opt.MapFrom(a => a.Items.Count));
        }
    }
}
