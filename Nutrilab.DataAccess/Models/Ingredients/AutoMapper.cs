using AutoMapper;
using Nutrilab.Dtos.Ingredients;

namespace Nutrilab.DataAccess.Models.Ingredients
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateIngredientDto, Ingredient>();
        }
    }
}
