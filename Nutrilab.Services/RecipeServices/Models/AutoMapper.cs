using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Services.RecipeServices.Models
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IRecipe, RecipeDetailsServiceModel>();
        }
    }
}
