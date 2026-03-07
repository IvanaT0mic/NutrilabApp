using Microsoft.JSInterop;
using System.Text.Json;

namespace NutrilabApp.Frontend.Services
{
    public class TokenService
    {
        private readonly IJSRuntime _js;

        public TokenService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string?> GetEmailAsync()
        {
            var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "token");
            if (string.IsNullOrEmpty(token)) return null;
            return ParseClaim(token, "email");
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "token");
            if (string.IsNullOrEmpty(token)) return [];
            var role = ParseClaim(token, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return role is null ? [] : [role];
        }

        private static string? ParseClaim(string token, string claimType)
        {
            try
            {
                var payload = token.Split('.')[1];
                var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
                var claims = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                return claims?.TryGetValue(claimType, out var val) == true ? val.GetString() : null;
            }
            catch { return null; }
        }
    }
}
