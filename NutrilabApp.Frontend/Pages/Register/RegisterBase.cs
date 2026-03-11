using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.Register
{
    public class RegisterBase : PageBase
    {
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected string Password { get; set; } = "";
        protected bool IsLoading { get; set; } = false;
        protected bool IsOnline { get; set; } = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsOnline = await JS.InvokeAsync<bool>("eval", "navigator.onLine");
                StateHasChanged();
            }
        }

        protected async Task HandleRegister()
        {
            IsOnline = await JS.InvokeAsync<bool>("eval", "navigator.onLine");
            if (!IsOnline)
            {
                Notifications.ShowError("You are offline. Please check your connection.");
                return;
            }

            IsLoading = true;

            var success = await AuthService.RegisterAsync(Email, Password);

            if (success)
            {
                Notifications.ShowSuccess("Account created! Redirecting to login...");
                await Task.Delay(1500);
                Navigation.NavigateTo("/login");
            }
            else
            {
                Notifications.ShowError("Registration failed. Email may already be in use.");
            }

            IsLoading = false;
        }
    }
}
