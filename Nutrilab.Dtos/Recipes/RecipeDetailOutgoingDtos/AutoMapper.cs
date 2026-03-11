using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Interfaces.ServiceModels;

namespace Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IRecipeDetailsServiceModel, RecipeDetailOutgoingDto>()
                .ForMember(
                    dest => dest.Ingredients,
                    opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(
                    dest => dest.ImageBase64,
                    opt => opt.MapFrom(src => src.Resources.FirstOrDefault().Base64 ?? null)
                );

            CreateMap<IRecipeIngredient, RecipeIngredientOutgoingDto>()
                .ForMember(
                    dest => dest.IngredientName,
                    opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(
                    dest => dest.Unit,
                    opt => opt.MapFrom(src => src.Ingredient.Unit));
        }
    }
}
