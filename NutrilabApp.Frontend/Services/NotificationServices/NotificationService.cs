using NutrilabApp.Frontend.Services.NotificationServices.Models;

namespace NutrilabApp.Frontend.Services.NotificationServices
{
    public class NotificationService
    {
        public event Action<NotificationMessage>? OnNotification;

        public void ShowSuccess(string message)
        {
            OnNotification?.Invoke(new NotificationMessage { Message = message, Type = NotificationType.Success });
        }

        public void ShowError(string message)
        {
            OnNotification?.Invoke(new NotificationMessage { Message = message, Type = NotificationType.Error });
        }
    }
}
