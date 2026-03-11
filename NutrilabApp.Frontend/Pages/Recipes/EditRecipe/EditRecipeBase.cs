using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Shared.Enums;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes.EditRecipe
{
    public class EditRecipeBase : PageBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;

        // Form fields
        protected string Name { get; set; } = "";
        protected string Description { get; set; } = "";
        protected int? PreparationTimeMinutes { get; set; }
        protected MealCategory? SelectedMealCategory { get; set; }
        protected DifficultyLvl? SelectedDifficultyLvl { get; set; }

        protected List<MealCategory> MealCategories { get; } = Enum.GetValues<MealCategory>().ToList();
        protected List<DifficultyLvl> DifficultyLevels { get; } = Enum.GetValues<DifficultyLvl>().ToList();

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            var roles = await TokenService.GetRolesAsync();
            bool canEdit = roles.Contains("Admin") || roles.Contains("Editor");
            if (!canEdit)
            {
                Navigation.NavigateTo($"/recipes/{Id}");
                return;
            }

            try
            {
                var recipe = await RecipeApiService.GetRecipeByIdAsync(Id);
                if (recipe == null)
                {
                    Navigation.NavigateTo("/recipes");
                    return;
                }

                Name = recipe.Name ?? "";
                Description = recipe.Description ?? "";
                PreparationTimeMinutes = recipe.PreparationTimeMinutes;
                SelectedMealCategory = recipe.MealCategory;
                SelectedDifficultyLvl = recipe.DifficultyLvl;
            }
            catch
            {
                Notifications.ShowError("Failed to load recipe.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Notifications.ShowError("Recipe name is required.");
                return;
            }

            IsSaving = true;

            var dto = new UpdateRecipeDto
            {
                Name = Name,
                Description = string.IsNullOrWhiteSpace(Description) ? null : Description,
                PreparationTimeMinutes = PreparationTimeMinutes,
                MealCategory = SelectedMealCategory,
                DifficultyLvl = SelectedDifficultyLvl
            };

            var success = await RecipeApiService.UpdateRecipeAsync(Id, dto);
            if (success)
            {
                Notifications.ShowSuccess("Recipe updated successfully!");
                await Task.Delay(1200);
                Navigation.NavigateTo($"/recipes/{Id}");
            }
            else
            {
                Notifications.ShowError("Failed to save changes.");
            }

            IsSaving = false;
        }

        protected void SetMealCategory(ChangeEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value?.ToString()))
                SelectedMealCategory = null;
            else if (Enum.TryParse<MealCategory>(e.Value.ToString(), out var val))
                SelectedMealCategory = val;
        }

        protected void SetDifficulty(ChangeEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value?.ToString()))
                SelectedDifficultyLvl = null;
            else if (Enum.TryParse<DifficultyLvl>(e.Value.ToString(), out var val))
                SelectedDifficultyLvl = val;
        }
    }
}