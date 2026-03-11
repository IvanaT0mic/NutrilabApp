using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;
using NutrilabApp.Frontend.Services.RecipeServices.Models;
using NutrilabApp.Frontend.Services.RoleServices;
using NutrilabApp.Frontend.Services.UserServices;

namespace NutrilabApp.Frontend.Pages.Dashboard
{
    public class DashboardBase : ComponentBase
    {
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected UserApiService UserApiService { get; set; } = default!;
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected RoleApiService RoleApiService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected List<string> Roles { get; set; } = new();
        protected string PrimaryRole => Roles.FirstOrDefault() ?? "User";
        protected bool IsAdmin => Roles.Contains("Admin");
        protected bool IsAdminOrMaintainer => Roles.Contains("Admin") || Roles.Contains("Maintainer");

        protected int UserCount { get; set; } = 0;
        protected int RecipeCount { get; set; } = 0;
        protected int RoleCount { get; set; } = 0;

        protected bool IsLoadingRecipes { get; set; } = false;
        protected List<RecipeCardDto> RecentRecipes { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            Email = await TokenService.GetEmailAsync() ?? "";
            Roles = await TokenService.GetRolesAsync();

            if (IsAdminOrMaintainer)
            {
                await LoadAdminStats();
            }
            else
            {
                await LoadUserRecipes();
            }
        }

        private async Task LoadAdminStats()
        {
            try
            {
                var usersTask = UserApiService.GetAllUsersAsync();
                var recipesTask = RecipeApiService.GetAllRecipesAsync();

                if (IsAdmin)
                {
                    var rolesTask = RoleApiService.GetAllRolesAsync();
                    await Task.WhenAll(usersTask, recipesTask, rolesTask);
                    RoleCount = rolesTask.Result?.Count ?? 0;
                }
                else
                {
                    await Task.WhenAll(usersTask, recipesTask);
                }

                UserCount = usersTask.Result?.Count ?? 0;
                RecipeCount = recipesTask.Result?.Count ?? 0;
            }
            catch { /* ignore stats errors */ }
        }

        private async Task LoadUserRecipes()
        {
            IsLoadingRecipes = true;
            try
            {
                var recipes = await RecipeApiService.GetAllRecipesAsync();
                RecentRecipes = recipes?.ToList() ?? new();
            }
            catch { }
            finally
            {
                IsLoadingRecipes = false;
            }
        }
    }
}
