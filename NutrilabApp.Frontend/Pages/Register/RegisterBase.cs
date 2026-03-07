using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.Register
{
    public class RegisterBase : ComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected string Password { get; set; } = "";
        protected string ErrorMessage { get; set; } = "";
        protected string SuccessMessage { get; set; } = "";
        protected bool IsLoading { get; set; } = false;

        protected async Task HandleRegister()
        {
            IsLoading = true;
            ErrorMessage = "";
            SuccessMessage = "";

            var success = await AuthService.RegisterAsync(Email, Password);

            if (success)
            {
                SuccessMessage = "Account created! Redirecting to login...";
                await Task.Delay(1500);
                Navigation.NavigateTo("/login");
            }
            else
            {
                ErrorMessage = "Registration failed. Email may already be in use.";
            }

            IsLoading = false;
        }
    }
}
