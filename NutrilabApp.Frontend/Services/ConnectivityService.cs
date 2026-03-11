using Microsoft.JSInterop;

namespace NutrilabApp.Frontend.Services
{
    public class ConnectivityService
    {
        private readonly IJSRuntime _js;
        public bool IsOnline { get; private set; } = true;

        public ConnectivityService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<bool> CheckAsync()
        {
            IsOnline = await _js.InvokeAsync<bool>("eval", "navigator.onLine");
            return IsOnline;
        }

        public async Task<bool> ShowOfflineAlertIfNeeded()
        {
            if (!await CheckAsync())
            {
                await _js.InvokeVoidAsync("alert", "You are offline. Please try again later.");
                return false;
            }
            return true;
        }
    }
}
