using Nutrilab.Dtos.Users;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.UserServices
{
    public class UserApiService
    {
        private readonly HttpClient _http;

        public UserApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDto>?> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDto>>("user");
        }

        public async Task<bool> UpdateUserRolesAsync(long userId, List<int> roleIds)
        {
            var response = await _http.PutAsJsonAsync($"user/{userId}/roles", new { roleIds });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var response = await _http.DeleteAsync($"user/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
