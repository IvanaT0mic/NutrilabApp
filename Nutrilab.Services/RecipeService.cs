using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.DataAccess.Models.RecipeResources;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Repositories;
using Nutrilab.Services.Handlers;
using Nutrilab.Services.Handlers.PdfHandlers;
using Nutrilab.Shared.Enums;
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
        Task<byte[]> DownloadRecipePdfByIdAsync(long id);
        Task<long> CreateImageAsync(long id, IFormFile file);
        Task UpdateAsync(long id, UpdateRecipeDto request);
        Task DeleteAsync(long id);
        Task DeleteImageByIdAsync(long id);
    }

    public sealed class RecipeService(
        IRecipeRepository repo,
        IIngredientRepository ingredientRepository,
        IRecipeIngredientRepository recipeIngredientRepository,
        ICurrentUserService currentUserService,
        IRecipeResourceRepository recipeResourceRepository,
        IFavouriteRecipeRepository favouriteRecipeRepository,
        PdfHandlerFactory pdfHandlerFactory,
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
            var recipe = await repo.GetByIdExtendedAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }

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

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (recipe.RecipeIngredients?.Any() == true)
            {
                await recipeIngredientRepository.DeleteRangeAsync(recipe.RecipeIngredients);
            }

            if (recipe.FavouriteUsers?.Any() == true)
            {
                await favouriteRecipeRepository.DeleteRangeAsync(recipe.FavouriteUsers);
            }

            if (recipe.Resources?.Any() == true)
            {
                await recipeResourceRepository.DeleteRangeAsync(recipe.Resources);
            }

            await repo.DeleteAsync(recipe);

            scope.Complete();
        }

        public async Task<long> CreateImageAsync(long id, IFormFile file)
        {
            var recipe = await repo.GetByIdExtendedAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }

            ValidateFile(file);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var base64 = Convert.ToBase64String(ms.ToArray());

            var resource = new RecipeResource
            {
                RecipeId = id,
                Base64 = base64
            };

            var resourceDb = await recipeResourceRepository.InsertAsync(resource);
            return resourceDb.Id;
        }

        public void ValidateFile(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var AllowedExtensions = new List<string>
                {
                   ".jpg",
                    ".jpeg",
                    ".png",
                    ".webp"
                };
            if (!AllowedExtensions.Contains(extension))
            {
                throw new BadRequestException("Unsupported file format");
            }
        }

        public async Task DeleteImageByIdAsync(long id)
        {
            var imgDb = await recipeResourceRepository.GetRecipeResourceAsync(id);
            if (imgDb == null)
            {
                throw new NotFoundException($"Recipe image with {id} not found");
            }

            await recipeResourceRepository.DeleteAsync(imgDb);
        }

        public async Task<byte[]> DownloadRecipePdfByIdAsync(long id)
        {
            var recipe = await repo.GetByIdExtendedAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe {id} not found");
            }

            var handler = pdfHandlerFactory.GetHandler(PdfReportType.Recipe);
            if (handler == null)
            {
                throw new BadRequestException("Handler not found");
            }
            return await handler.GenerateAsync(id);
        }
    }
}
