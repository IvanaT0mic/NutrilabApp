using NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers.Models;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers
{
    public class ErrorInterceptor : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = await TryReadErrorMessageAsync(response);
                throw new ApiException(message, (int)response.StatusCode);
            }

            return response;
        }

        private static async Task<string> TryReadErrorMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content.ReadFromJsonAsync<CustomErrorBody>();
                if (!string.IsNullOrWhiteSpace(body?.Message)) return body.Message;
                if (!string.IsNullOrWhiteSpace(body?.Title)) return body.Title;
            }
            catch { }

            try
            {
                var raw = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(raw)) return raw;
            }
            catch { }

            return $"Request failed ({(int)response.StatusCode})";
        }
    }
}
