using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Layout
{
    public class MainLayoutBase : LayoutComponentBase, IDisposable
    {
        [Inject] protected IJSRuntime JS { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected List<string> Roles { get; set; } = new();
        protected string CurrentPage { get; set; } = "";
        protected bool IsReady { get; set; } = false;
        protected bool IsOnline { get; set; } = true;

        protected bool IsAdmin => IsReady && Roles.Contains("Admin");
        protected bool IsAdminOrMaintainer => IsReady && (Roles.Contains("Admin") || Roles.Contains("Maintainer"));
        protected bool IsEditorOrAdmin => IsReady && (Roles.Contains("Admin") || Roles.Contains("Editor"));
        protected string PrimaryRole => IsReady ? (Roles.FirstOrDefault() ?? "User") : "";

        protected override async Task OnInitializedAsync()
        {
            Navigation.LocationChanged += OnLocationChanged;

            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            Email = await TokenService.GetEmailAsync() ?? "";
            Roles = await TokenService.GetRolesAsync();
            UpdateCurrentPage();

            IsReady = true;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateCurrentPage();
            InvokeAsync(StateHasChanged);
        }

        private void UpdateCurrentPage()
        {
            var uri = new Uri(Navigation.Uri);
            var segments = uri.AbsolutePath.Trim('/').Split('/');
            CurrentPage = segments.FirstOrDefault() ?? "";
        }

        protected async Task Logout()
        {
            await AuthService.LogoutAsync();
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsOnline = await JS.InvokeAsync<bool>("eval", "navigator.onLine");
                await JS.InvokeVoidAsync("registerConnectivityEvents", DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public async Task SetOnlineStatus(bool isOnline)
        {
            IsOnline = isOnline;
            await InvokeAsync(StateHasChanged);
        }
    }
}