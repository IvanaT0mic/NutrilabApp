using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Repositories;
using Nutrilab.Shared.Models.Exceptions;
using System.Transactions;

namespace Nutrilab.Services
{
    public interface IRecipeIngredientService
    {
        Task UpateRecipeIngreditents(long recipeId, List<RecipeIngredientDto> request);
    }

    public sealed class RecipeIngredientService(
        IRecipeRepository recipeRepository,
        IIngredientRepository ingredientRepository,
        IRecipeIngredientRepository recipeIngredientRepository
        ) : IRecipeIngredientService
    {
        public async Task UpateRecipeIngreditents(long recipeId, List<RecipeIngredientDto> request)
        {
            var recipe = await recipeRepository.GetByIdExtendedAsync(recipeId);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {recipeId} not found");
            }

            if (request.Count == 0)
            {
                await recipeIngredientRepository.DeleteRangeAsync(recipe.RecipeIngredients);
                return;
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var ingredientIds = request.Select(x => x.IngredientId).ToList();
            var existingIngredients = await ingredientRepository.GetAllByIdsAsync(ingredientIds);

            if (existingIngredients.Count != ingredientIds.Count)
            {
                var missing = ingredientIds.Except(existingIngredients.Select(i => i.Id));
                throw new NotFoundException($"Ingredients not found: {string.Join(", ", missing)}");
            }

            if (recipe.RecipeIngredients.Count != 0)
            {
                await recipeIngredientRepository.DeleteRangeAsync(recipe.RecipeIngredients);
            }

            var recipeIngredients = request.Select(i =>
                new RecipeIngredient
                {
                    RecipeId = recipeId,
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity
                }).ToList();

            await recipeIngredientRepository.InsertRangeAsync(recipeIngredients);

            scope.Complete();
        }
    }
}
