using Microsoft.AspNetCore.Components;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;
using NutrilabApp.Frontend.Services.RecipeServices.Models;

namespace NutrilabApp.Frontend.Pages.Recipes
{
    public class RecipeLibraryBase : ComponentBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected List<RecipeCardDto> AllRecipes { get; set; } = new();
        protected bool IsLoading { get; set; } = true;

        private string _searchQuery = "";
        protected string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value; StateHasChanged(); }
        }

        protected string ActiveCategory { get; set; } = "All";
        protected List<string> Categories { get; } = new() { "All", "Breakfast", "Lunch", "Dinner" };

        protected List<RecipeCardDto> FilteredRecipes =>
            AllRecipes
                .Where(r => string.IsNullOrEmpty(SearchQuery) ||
                            r.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            try
            {
                AllRecipes = await RecipeApiService.GetAllRecipesAsync() ?? new();
            }
            catch { }
            finally
            {
                IsLoading = false;
            }
        }

        protected void SetCategory(string category)
        {
            ActiveCategory = category;
        }
    }
}
