using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services.NotificationServices;
using NutrilabApp.Frontend.Services.NotificationServices.Models;

namespace NutrilabApp.Frontend.Services.SnackBarServices
{
    public partial class Snackbar : IDisposable
    {
        [Inject] private NotificationService Notifications { get; set; } = default!;

        private readonly List<ToastItem> _toasts = new();

        protected override void OnInitialized()
        {
            Notifications.OnNotification += HandleNotification;
        }

        private void HandleNotification(NotificationMessage msg)
        {
            var toast = new ToastItem
            {
                Message = msg.Message,
                Type = msg.Type
            };

            _toasts.Add(toast);
            InvokeAsync(StateHasChanged);

            _ = Task.Delay(4000).ContinueWith(_ =>
            {
                toast.Exiting = true;
                InvokeAsync(StateHasChanged);

                return Task.Delay(250).ContinueWith(_ =>
                {
                    _toasts.Remove(toast);
                    InvokeAsync(StateHasChanged);
                });
            });
        }

        private void Remove(ToastItem toast)
        {
            toast.Exiting = true;
            StateHasChanged();
            _ = Task.Delay(250).ContinueWith(_ =>
            {
                _toasts.Remove(toast);
                InvokeAsync(StateHasChanged);
            });
        }

        public void Dispose()
        {
            Notifications.OnNotification -= HandleNotification;
        }

        private class ToastItem
        {
            public string Message { get; set; } = "";
            public NotificationType Type { get; set; }
            public bool Exiting { get; set; }
        }
    }
}