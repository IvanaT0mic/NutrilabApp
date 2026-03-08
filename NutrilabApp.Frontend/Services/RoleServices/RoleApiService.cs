using Nutrilab.Dtos.Roles;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.RoleServices
{
    public class RoleApiService
    {
        private readonly HttpClient _http;

        public RoleApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RoleDto>?> GetAllRolesAsync()
        {
            return await _http.GetFromJsonAsync<List<RoleDto>>("roles");
        }
    }
}
