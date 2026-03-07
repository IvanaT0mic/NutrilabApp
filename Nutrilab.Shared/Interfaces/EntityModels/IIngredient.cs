namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IIngredient
    {
        long Id { get; }

        string Name { get; }

        string Unit { get; }

        List<IRecipeIngredient> RecipeIngredients { get; }
    }
}
