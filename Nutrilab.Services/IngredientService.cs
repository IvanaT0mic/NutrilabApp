using AutoMapper;
using Nutrilab.DataAccess.Models.Ingredients;
using Nutrilab.Dtos.Ingredients;
using Nutrilab.Repositories;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Models.Exceptions;

namespace Nutrilab.Services
{
    public interface IIngredientService
    {
        Task<List<IIngredient>> GetAllAsync();
        Task<long> CreateAsync(CreateIngredientDto request);
        Task<IIngredient> GetByIdAsync(long id);
        Task DeleteAsync(long id);
    }

    public sealed class IngredientService(IIngredientRepository repo, IMapper mapper) : IIngredientService
    {
        public async Task<List<IIngredient>> GetAllAsync()
        {
            var ingredients = await repo.GetAllAsync();
            return ingredients.ToList<IIngredient>();
        }

        public async Task<IIngredient> GetByIdAsync(long id)
        {
            var ingredientDb = await repo.GetByIdAsync(id);
            if (ingredientDb == null)
            {
                throw new NotFoundException("Ingredient with id " + id + " not found.");
            }
            return ingredientDb;
        }

        public async Task<long> CreateAsync(CreateIngredientDto request)
        {
            var ingredient = mapper.Map<Ingredient>(request);
            Ingredient ingredientdb = await repo.InsertAsync(ingredient);
            return ingredientdb.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var ingredient = await repo.GetByIdAsync(id);
            if (ingredient == null)
            {
                throw new NotFoundException($"Ingredient {id} not found");
            }
            await repo.DeleteAsync(ingredient);
        }
    }
}
