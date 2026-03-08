using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes
{
    public class RecipeDetailBase : ComponentBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected RecipeDetailOutgoingDto? Recipe { get; set; }
        protected bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            try
            {
                Recipe = await RecipeApiService.GetRecipeByIdAsync(Id);
            }
            catch { }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
