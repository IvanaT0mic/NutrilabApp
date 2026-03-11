using Nutrilab.Dtos.Users;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class UserApiService(HttpClient http)
    {
        public async Task<List<UserDto>?> GetAllUsersAsync()
        {
            return await http.GetFromJsonAsync<List<UserDto>>("user");
        }

        public async Task<bool> UpdateUserRolesAsync(long userId, List<int> roleIds)
        {
            var response = await http.PutAsJsonAsync($"user/{userId}/roles", new { roleIds });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var response = await http.DeleteAsync($"user/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
