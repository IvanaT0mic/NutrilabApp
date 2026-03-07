using Nutrilab.DataAccess.Models.Ingredients;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.RecipeIngredients
{
    public class RecipeIngredient : IRecipeIngredient
    {
        public long Id { get; set; }

        public long RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public long IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public decimal Quantity { get; set; }

        IRecipe IRecipeIngredient.Recipe => Recipe;

        IIngredient IRecipeIngredient.Ingredient => Ingredient;
    }
}
