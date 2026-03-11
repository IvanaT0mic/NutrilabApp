using Nutrilab.Dtos.Ingredients;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class IngredientApiService(HttpClient http)
    {
        public async Task<List<IngredientOutgoingDto>?> GetAllAsync()
        {
            return await http.GetFromJsonAsync<List<IngredientOutgoingDto>>("ingredients");
        }

        public async Task<long?> CreateAsync(CreateIngredientDto dto)
        {
            var response = await http.PostAsJsonAsync("ingredients", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<long>();
        }
    }
}
