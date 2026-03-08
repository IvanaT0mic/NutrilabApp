using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IRecipe, RecipeDetailOutgoingDto>()
                        .ForMember(
                            dest => dest.Ingredients,
                            opt => opt.MapFrom(src => src.RecipeIngredients));

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
