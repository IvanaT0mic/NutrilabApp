using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigation;

        public AuthService(HttpClient http, IJSRuntime js, NavigationManager navigation)
        {
            _http = http;
            _js = js;
            _navigation = navigation;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("auth/login", new { email, password });
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            await _js.InvokeVoidAsync("sessionStorage.setItem", "token", result!.Access);
            return true;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("auth/register", new { email, password });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "token");
            return !string.IsNullOrEmpty(token);
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "token");
            _navigation.NavigateTo("/login");
        }

        private class TokenResponse
        {
            public string Access { get; set; } = "";
        }
    }
}
