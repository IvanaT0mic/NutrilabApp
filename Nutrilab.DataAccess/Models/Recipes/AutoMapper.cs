using AutoMapper;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;

namespace Nutrilab.DataAccess.Models.Recipes
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateRecipeDto, Recipe>();
        }
    }
}
