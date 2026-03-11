namespace NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers.Models
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public ApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
