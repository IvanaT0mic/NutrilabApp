using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.Login
{
    public class LoginBase : PageBase
    {
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected string Password { get; set; } = "";
        protected bool IsLoading { get; set; } = false;
        protected bool IsOnline { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            if (await AuthService.IsAuthenticatedAsync())
                Navigation.NavigateTo("/dashboard");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsOnline = await JS.InvokeAsync<bool>("eval", "navigator.onLine");
                StateHasChanged();
            }
        }

        protected async Task HandleLogin()
        {
            IsOnline = await JS.InvokeAsync<bool>("eval", "navigator.onLine");
            if (!IsOnline)
            {
                Notifications.ShowError("You are offline. Please check your connection.");
                return;
            }

            IsLoading = true;

            var success = await AuthService.LoginAsync(Email, Password);

            if (success)
                Navigation.NavigateTo("/dashboard");
            else
            {
                Notifications.ShowError("Invalid email or password.");
            }

            IsLoading = false;
        }
    }
}