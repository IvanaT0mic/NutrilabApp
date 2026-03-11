using NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers.Models;

namespace NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers
{
    public sealed class PageActionGuard(ConnectivityService connectivity)
    {
        /// <summary>
        /// Throws <see cref="OfflineException"/> if the browser is currently offline.
        /// Call this at the top of every Save / Delete / POST / PUT action.
        /// </summary>
        public async Task GuardAsync()
        {
            var isOnline = await connectivity.CheckAsync();
            if (!isOnline)
                throw new OfflineException();
        }
    }
}
