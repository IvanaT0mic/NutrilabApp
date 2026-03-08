using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Ingredients
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IIngredient, IngredientOutgoingDto>();
        }
    }
}
