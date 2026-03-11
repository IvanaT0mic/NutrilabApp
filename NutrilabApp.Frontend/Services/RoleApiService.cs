using Nutrilab.Dtos.Permissions;
using Nutrilab.Dtos.Roles;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class RoleApiService(HttpClient http)
    {
        public async Task<List<PermissionDto>?> GetAllPermissionsAsync()
        {
            return await http.GetFromJsonAsync<List<PermissionDto>>("role/permissions");
        }

        public async Task<bool> UpdateRolePermissionsAsync(int id, UpdateRolePermissionsDto request)
        {
            var response = await http.PutAsJsonAsync($"role/{id}/permissions", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<RoleDto>?> GetAllRolesAsync()
        {
            return await http.GetFromJsonAsync<List<RoleDto>>("roles");
        }
    }
}
