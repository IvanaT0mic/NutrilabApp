using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Shared.Interfaces.ServiceModels
{
    public interface IRecipeDetailsServiceModel : IRecipe
    {
        public bool IsFavourite { get; set; }
    }
}
