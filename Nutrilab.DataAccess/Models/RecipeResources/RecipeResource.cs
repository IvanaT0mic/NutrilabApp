using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.Shared.Interfaces.EntityAudit;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.RecipeResources
{
    public class RecipeResource : IRecipeResource
    {
        public long Id { get; set; }

        public string Base64 { get; set; }

        public long RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        IRecipe IRecipeResource.Recipe => Recipe;
    }
}
