using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Layout
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }
            Email = await TokenService.GetEmailAsync() ?? "";
        }

        protected async Task Logout()
        {
            await AuthService.LogoutAsync();
        }
    }
}
