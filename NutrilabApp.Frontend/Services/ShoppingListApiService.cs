using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class ShoppingListApiService(HttpClient http)
    {
        public async Task<List<ShoppingListOutgoingDto>?> GetAllAsync()
        {
            return await http.GetFromJsonAsync<List<ShoppingListOutgoingDto>>("shoppinglists");
        }

        public async Task<ShoppingListDetailOutgoingDto?> GetByIdAsync(long id)
        {
            return await http.GetFromJsonAsync<ShoppingListDetailOutgoingDto>($"shoppinglists/{id}");
        }

        public async Task<long?> CreateAsync(CreateShoppingListDto dto)
        {
            var response = await http.PostAsJsonAsync("shoppinglists", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<long>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await http.DeleteAsync($"shoppinglists/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ShoppingListItemOutgoingDto?> AddItemAsync(long listId, CreateShoppingListItemDto dto)
        {
            var response = await http.PostAsJsonAsync($"shoppinglists/{listId}/items", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ShoppingListItemOutgoingDto>();
        }

        public async Task<bool> UpdateItemAsync(long listId, long itemId, UpdateShoppingListItemDto dto)
        {
            var response = await http.PutAsJsonAsync($"shoppinglists/{listId}/items/{itemId}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(long listId, long itemId)
        {
            var response = await http.DeleteAsync($"shoppinglists/{listId}/items/{itemId}");
            return response.IsSuccessStatusCode;
        }
    }
}
