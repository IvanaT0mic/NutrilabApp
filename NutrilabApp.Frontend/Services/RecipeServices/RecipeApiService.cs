using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using NutrilabApp.Frontend.Services.RecipeServices.Models;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.RecipeServices
{
    public class RecipeApiService(HttpClient http)
    {
        public async Task<List<RecipeCardDto>?> GetAllRecipesAsync()
        {
            return await http.GetFromJsonAsync<List<RecipeCardDto>>("recipes");
        }

        public async Task<RecipeDetailOutgoingDto?> GetRecipeByIdAsync(long id)
        {
            return await http.GetFromJsonAsync<RecipeDetailOutgoingDto>($"recipes/{id}");
        }

        public async Task<long?> CreateRecipeAsync(CreateRecipeDto dto)
        {
            var response = await http.PostAsJsonAsync("recipes", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<long>();
        }

        public async Task<bool> UpdateRecipeAsync(long id, UpdateRecipeDto dto)
        {
            var response = await http.PutAsJsonAsync($"recipes/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> MarkAsFavouriteAsync(long id)
        {
            var response = await http.PostAsync($"recipes/{id}/favourite", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveFromFavouritesAsync(long id)
        {
            var response = await http.DeleteAsync($"recipes/{id}/favourite");
            return response.IsSuccessStatusCode;
        }

        public async Task<byte[]?> DownloadPdfAsync(long id)
        {
            var response = await http.GetAsync($"recipes/{id}/pdf");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<bool> UploadImageAsync(long id, Stream fileStream, string fileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileStream), "File", fileName);
            var response = await http.PostAsync($"recipes/{id}/image", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRecipeAsync(long id)
        {
            var response = await http.DeleteAsync($"recipes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
