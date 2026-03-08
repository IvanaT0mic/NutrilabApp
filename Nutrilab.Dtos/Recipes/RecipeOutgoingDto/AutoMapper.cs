using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Recipes.RecipeOutgoingDto
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IRecipe, RecipeOutgoingDto>();
        }
    }
}
