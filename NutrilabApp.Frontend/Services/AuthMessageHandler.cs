using Microsoft.JSInterop;

namespace NutrilabApp.Frontend.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthMessageHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "token");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
