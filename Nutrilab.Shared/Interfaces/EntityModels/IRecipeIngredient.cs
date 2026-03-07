namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IRecipeIngredient
    {
        long Id { get; }

        long RecipeId { get; }

        IRecipe Recipe { get; }

        long IngredientId { get; }

        IIngredient Ingredient { get; }

        decimal Quantity { get; }
    }
}
