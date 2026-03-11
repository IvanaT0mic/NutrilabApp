using Microsoft.AspNetCore.Components;
using Nutrilab.Shared.Enums;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;
using NutrilabApp.Frontend.Services.RecipeServices.Models;

namespace NutrilabApp.Frontend.Pages.Recipes
{
    public class RecipeLibraryBase : PageBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;

        protected List<RecipeCardDto> AllRecipes { get; set; } = new();
        protected List<RecipeCardDto> FavouriteRecipes { get; set; } = new();

        protected bool IsLoading { get; set; } = true;
        protected bool IsFavouritesLoading { get; set; } = true;


        private string _searchQuery = "";
        protected string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value; StateHasChanged(); }
        }

        protected string ActiveTab { get; set; } = "All";
        protected string ActiveCategory { get; set; } = "All";
        protected List<string> Categories { get; } = new() { "All", "Breakfast", "Lunch", "Dinner", "Snack", "Dessert" };

        protected List<RecipeCardDto> FilteredRecipes
        {
            get
            {
                IEnumerable<RecipeCardDto> result =
                    ActiveTab == "Favourites"
                    ? FavouriteRecipes
                    : AllRecipes;

                if (!string.IsNullOrEmpty(SearchQuery))
                    result = result.Where(r => r.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

                if (ActiveCategory != "All")
                {
                    if (Enum.TryParse<MealCategory>(ActiveCategory.ToUpper(), out var cat))
                        result = result.Where(r => r.MealCategory == cat);
                }

                return result.ToList();
            }
        }

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
                FavouriteRecipes = await RecipeApiService.GetAllMineFavouriteRecipesAsync() ?? new();
            }
            catch { }
            finally
            {
                IsLoading = false;
                IsFavouritesLoading = false;
            }
        }

        protected void SetTab(string tab)
        {
            ActiveTab = tab;
        }

        protected void SetCategory(string category)
        {
            ActiveCategory = category;
        }
    }
}