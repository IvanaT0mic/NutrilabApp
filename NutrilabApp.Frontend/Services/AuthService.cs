using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class AuthService(HttpClient http, IJSRuntime js, NavigationManager navigation)
    {
        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await http.PostAsJsonAsync("auth/login", new { email, password });
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            await js.InvokeVoidAsync("sessionStorage.setItem", "token", result!.Access);
            return true;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var response = await http.PostAsJsonAsync("auth/register", new { email, password });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await js.InvokeAsync<string>("sessionStorage.getItem", "token");
            return !string.IsNullOrEmpty(token);
        }

        public async Task LogoutAsync()
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", "token");
            navigation.NavigateTo("/login");
        }

        private class TokenResponse
        {
            public string Access { get; set; } = "";
        }
    }
}
