using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected string Password { get; set; } = "";
        protected string ErrorMessage { get; set; } = "";
        protected bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (await AuthService.IsAuthenticatedAsync())
                Navigation.NavigateTo("/dashboard");
        }

        protected async Task HandleLogin()
        {
            IsLoading = true;
            ErrorMessage = "";

            var success = await AuthService.LoginAsync(Email, Password);

            if (success)
                Navigation.NavigateTo("/dashboard");
            else
                ErrorMessage = "Invalid email or password.";

            IsLoading = false;
        }
    }
}