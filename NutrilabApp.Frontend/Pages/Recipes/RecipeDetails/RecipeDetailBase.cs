using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes.RecipeDetails
{
    public class RecipeDetailBase : PageBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected RecipeDetailOutgoingDto? Recipe { get; set; }
        protected bool IsLoading { get; set; } = true;
        protected bool IsFavouriteLoading { get; set; } = false;
        protected bool IsDownloading { get; set; } = false;
        protected bool CanEdit { get; set; } = false;

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

            try
            {
                bool success;
                if (Recipe.IsFavourite)
                    success = await RecipeApiService.RemoveFromFavouritesAsync(Id);
                else
                    success = await RecipeApiService.MarkAsFavouriteAsync(Id);

                if (success)
                {
                    Recipe.IsFavourite = !Recipe.IsFavourite;
                    Notifications.ShowSuccess(Recipe.IsFavourite ? "Added to favourites!" : "Removed from favourites.");
                    _ = Task.Delay(500).ContinueWith(_ => { InvokeAsync(StateHasChanged); });
                }
                else
                {
                    Notifications.ShowError("Could not update favourites.");
                }
            }
            catch
            {
                Notifications.ShowError("Could not update favourites.");
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
                    Notifications.ShowError("Failed to download PDF.");
                }
            }
            catch
            {
                Notifications.ShowError("Failed to download PDF.");
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