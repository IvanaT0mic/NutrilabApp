using AutoMapper;
using Nutrilab.Dtos.Recipes.RecipeOutgoingDto;
using Nutrilab.Shared.Interfaces.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IShoppingList, ShoppingListOutgoingDto>();
        }
    }
}
