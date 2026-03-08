using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using NutrilabApp.Frontend.Services.RecipeServices.Models;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.RecipeServices
{
    public class RecipeApiService
    {
        private readonly HttpClient _http;

        public RecipeApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RecipeCardDto>?> GetAllRecipesAsync()
        {
            return await _http.GetFromJsonAsync<List<RecipeCardDto>>("recipes");
        }

        public async Task<RecipeDetailOutgoingDto?> GetRecipeByIdAsync(long id)
        {
            return await _http.GetFromJsonAsync<RecipeDetailOutgoingDto>($"recipes/{id}");
        }
    }
}
