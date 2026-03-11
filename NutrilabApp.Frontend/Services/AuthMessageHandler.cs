using Microsoft.JSInterop;

namespace NutrilabApp.Frontend.Services
{
    public class AuthMessageHandler(IJSRuntime js) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await js.InvokeAsync<string>("sessionStorage.getItem", "token");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
