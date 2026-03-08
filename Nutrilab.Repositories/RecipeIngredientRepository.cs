using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.RecipeIngredients;

namespace Nutrilab.Repositories
{
    public interface IRecipeIngredientRepository
    {
        Task<List<RecipeIngredient>> InsertRangeAsync(List<RecipeIngredient> data);
        Task DeleteRangeAsync(List<RecipeIngredient> data);
    }

    public sealed class RecipeIngredientRepository(EntityContext context)
        : BaseRepository<RecipeIngredient>(context), IRecipeIngredientRepository
    {
    }
}
