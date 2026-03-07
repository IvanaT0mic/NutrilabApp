using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.Ingredients
{
    public class Ingredient : IIngredient
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }

        List<IRecipeIngredient> IIngredient.RecipeIngredients => RecipeIngredients.ToList<IRecipeIngredient>();
    }
}
