using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.Dtos.Recipes;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Repositories;
using Nutrilab.Shared.Models.Exceptions;

namespace Nutrilab.Services
{
    public interface IRecipeService
    {
        Task<List<RecipeOutgoingDto>> GetAllAsync();
        Task<RecipeDetailOutgoingDto> GetByIdAsync(long id);
        Task<RecipeDetailOutgoingDto> CreateAsync(CreateRecipeDto request, long userId);
        Task<RecipeDetailOutgoingDto> UpdateAsync(long id, UpdateRecipeDto request, long userId);
        Task DeleteAsync(long id, long userId);
    }

    public sealed class RecipeService(IRecipeRepository repo) : IRecipeService
    {
        public async Task<List<RecipeOutgoingDto>> GetAllAsync()
        {
            var recipes = await repo.GetAllAsync();
            return recipes.Select(r => new RecipeOutgoingDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();
        }

        public async Task<RecipeDetailOutgoingDto> GetByIdAsync(long id)
        {
            var recipe = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Recipe {id} not found");
            return MapDetail(recipe);
        }

        public async Task<RecipeDetailOutgoingDto> CreateAsync(CreateRecipeDto request, long userId)
        {
            var recipe = new Recipe
            {
                Name = request.Name,
                Description = request.Description,
                CreatedByUserId = userId,
                RecipeIngredients = request.Ingredients.Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var created = await repo.InsertAsync(recipe);
            return await GetByIdAsync(created.Id);
        }

        public async Task<RecipeDetailOutgoingDto> UpdateAsync(long id, UpdateRecipeDto request, long userId)
        {
            var recipe = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Recipe {id} not found");

            if (recipe.CreatedByUserId != userId)
                throw new UnauthorizedException("You can only edit your own recipes");

            recipe.Name = request.Name;
            recipe.Description = request.Description;

            await repo.UpdateAsync(recipe);
            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(long id, long userId)
        {
            var recipe = await repo.GetByIdAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }

            if (recipe.CreatedByUserId != userId)
            {
                throw new UnauthorizedException("You can only delete your own recipes");
            }

            await repo.DeleteAsync(recipe);
        }

        private static RecipeDetailOutgoingDto MapDetail(Recipe r) => new()
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Ingredients = r.RecipeIngredients?.Select(i => new RecipeIngredientOutgoingDto
            {
                IngredientId = i.IngredientId,
                IngredientName = i.Ingredient?.Name ?? "",
                Quantity = i.Quantity,
                Unit = i.Ingredient?.Unit ?? ""
            }).ToList() ?? []
        };
    }
}
