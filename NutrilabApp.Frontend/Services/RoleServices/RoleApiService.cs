using Nutrilab.Dtos.Roles;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.RoleServices
{
    public class RoleApiService(HttpClient http)
    {
        public async Task<List<RoleDto>?> GetAllRolesAsync()
        {
            return await http.GetFromJsonAsync<List<RoleDto>>("roles");
        }
    }
}
