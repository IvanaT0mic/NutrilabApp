using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Shared.Interfaces.EntityAudit
{
    public interface IRecipeResource
    {
        long Id { get; }

        string Base64 { get; }

        long RecipeId { get; }

        IRecipe Recipe { get; }
    }
}
