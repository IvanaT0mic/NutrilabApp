using AutoMapper;
using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Repositories;
using Nutrilab.Services.Handlers;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Models.Exceptions;
using System.Transactions;

namespace Nutrilab.Services
{
    public interface IRecipeService
    {
        Task<List<IRecipe>> GetAllAsync();
        Task<IRecipe> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateRecipeDto request);
        Task UpdateAsync(long id, UpdateRecipeDto request);
        Task DeleteAsync(long id);
    }

    public sealed class RecipeService(
        IRecipeRepository repo,
        IIngredientRepository ingredientRepository,
        IRecipeIngredientRepository recipeIngredientRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
        ) : IRecipeService
    {
        public async Task<List<IRecipe>> GetAllAsync()
        {
            var recipes = await repo.GetAllAsync();
            return recipes.ToList<IRecipe>();
        }

        public async Task<IRecipe> GetByIdAsync(long id)
        {
            var recipe = await repo.GetByIdExtendedAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }
            return recipe;
        }

        public async Task<long> CreateAsync(CreateRecipeDto request)
        {
            var ingredientIds = request.Ingredients.Select(i => i.IngredientId).ToList();
            var existingIngredients = await ingredientRepository.GetAllByIdsAsync(ingredientIds);

            if (existingIngredients.Count != ingredientIds.Count)
            {
                var missing = ingredientIds.Except(existingIngredients.Select(i => i.Id));
                throw new NotFoundException($"Ingredients not found: {string.Join(", ", missing)}");
            }

            var recipe = mapper.Map<Recipe>(request);
            recipe.CreatedByUserId = currentUserService.GetCurrentUser().Id;

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var created = await repo.InsertAsync(recipe);
            await InsertRecipeIngredientsIfAnyAsync(created.Id, request.Ingredients);

            scope.Complete();
            return created.Id;
        }

        private async Task InsertRecipeIngredientsIfAnyAsync(long recipeId, List<RecipeIngredientDto> ingredients)
        {
            if (ingredients.Count == 0) return;

            var recipeIngredients = ingredients.Select(i => new RecipeIngredient
            {
                RecipeId = recipeId,
                IngredientId = i.IngredientId,
                Quantity = i.Quantity
            }).ToList();

            await recipeIngredientRepository.InsertRangeAsync(recipeIngredients);
        }

        public async Task UpdateAsync(long id, UpdateRecipeDto request)
        {
            var recipe = await repo.GetByIdExtendedAsync(id)
                ?? throw new NotFoundException($"Recipe {id} not found");


            var currentUser = currentUserService.GetCurrentUser();
            if (recipe.CreatedByUserId != currentUser.Id)
            {
                throw new UnauthorizedException("You can only edit your own recipes");
            }

            recipe.Name = request.Name;
            recipe.Description = request.Description;

            await repo.UpdateAsync(recipe);
        }

        public async Task DeleteAsync(long id)
        {
            var recipe = await repo.GetByIdExtendedAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }

            var currentUser = currentUserService.GetCurrentUser();
            if (recipe.CreatedByUserId != currentUser.Id)
            {
                throw new UnauthorizedException("You can only delete your own recipes");
            }

            await repo.DeleteAsync(recipe);
        }
    }
}
