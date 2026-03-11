namespace NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers.Models
{
    public sealed class OfflineException : Exception
    {
        public OfflineException()
            : base("You are offline. Please try again when your connection is restored.") { }
    }
}
