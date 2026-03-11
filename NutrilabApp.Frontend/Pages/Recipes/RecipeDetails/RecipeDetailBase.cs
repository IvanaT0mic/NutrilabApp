using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes.RecipeDetails
{
    public class RecipeDetailBase : ComponentBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected RecipeDetailOutgoingDto? Recipe { get; set; }
        protected bool IsLoading { get; set; } = true;
        protected bool IsFavourite { get; set; } = false;
        protected bool IsFavouriteLoading { get; set; } = false;
        protected bool IsDownloading { get; set; } = false;
        protected bool CanEdit { get; set; } = false;
        protected string? ErrorMessage { get; set; }
        protected string? SuccessMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            var roles = await TokenService.GetRolesAsync();
            CanEdit = roles.Contains("Admin") || roles.Contains("Editor");

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

        protected async Task ToggleFavourite()
        {
            if (IsFavouriteLoading) return;
            IsFavouriteLoading = true;
            ErrorMessage = null;

            try
            {
                bool success;
                if (IsFavourite)
                    success = await RecipeApiService.RemoveFromFavouritesAsync(Id);
                else
                    success = await RecipeApiService.MarkAsFavouriteAsync(Id);

                if (success)
                {
                    IsFavourite = !IsFavourite;
                    SuccessMessage = IsFavourite ? "Added to favourites!" : "Removed from favourites.";
                    _ = Task.Delay(2000).ContinueWith(_ => { SuccessMessage = null; InvokeAsync(StateHasChanged); });
                }
                else
                {
                    ErrorMessage = "Could not update favourites.";
                }
            }
            catch
            {
                ErrorMessage = "Could not update favourites.";
            }
            finally
            {
                IsFavouriteLoading = false;
            }
        }

        protected async Task DownloadPdf()
        {
            if (IsDownloading) return;
            IsDownloading = true;
            ErrorMessage = null;

            try
            {
                var pdfBytes = await RecipeApiService.DownloadPdfAsync(Id);
                if (pdfBytes != null)
                {
                    var base64 = Convert.ToBase64String(pdfBytes);
                    var fileName = $"recipe_{Id}.pdf";
                    await JS.InvokeVoidAsync("downloadFileFromBase64", base64, fileName, "application/pdf");
                }
                else
                {
                    ErrorMessage = "Failed to download PDF.";
                }
            }
            catch
            {
                ErrorMessage = "Failed to download PDF.";
            }
            finally
            {
                IsDownloading = false;
            }
        }

        protected string GetDifficultyLabel(Nutrilab.Shared.Enums.DifficultyLvl? lvl) => lvl switch
        {
            Nutrilab.Shared.Enums.DifficultyLvl.EASY => "Easy",
            Nutrilab.Shared.Enums.DifficultyLvl.MEDIUM => "Medium",
            Nutrilab.Shared.Enums.DifficultyLvl.HARD => "Hard",
            _ => ""
        };

        protected string GetDifficultyIcon(Nutrilab.Shared.Enums.DifficultyLvl? lvl) => lvl switch
        {
            Nutrilab.Shared.Enums.DifficultyLvl.EASY => "✅",
            Nutrilab.Shared.Enums.DifficultyLvl.MEDIUM => "⚡",
            Nutrilab.Shared.Enums.DifficultyLvl.HARD => "🔥",
            _ => ""
        };
    }
}