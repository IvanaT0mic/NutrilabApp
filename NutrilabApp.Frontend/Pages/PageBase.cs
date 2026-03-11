using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers;
using NutrilabApp.Frontend.Services.NotificationServices;

namespace NutrilabApp.Frontend.Pages
{
    public abstract class PageBase : ComponentBase
    {
        [Inject] protected PageActionGuard ActionGuard { get; set; } = default!;
        [Inject] protected NotificationService Notifications { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;

        protected async Task<bool> ForbbitOnlineActionsAsync()
        {
            try
            {
                await ActionGuard.GuardAsync();
                return false;
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex.Message);
                return true;
            }
        }
    }
}
