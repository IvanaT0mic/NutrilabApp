using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Ingredients;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Shared.Enums;
using NutrilabApp.Frontend.Pages.Recipes.CreateRecipe.Models;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes.EditRecipe
{
    public class EditRecipeBase : PageBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected IngredientApiService IngredientApiService { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;

        // Recipe fields
        protected string Name { get; set; } = "";
        protected string Description { get; set; } = "";
        protected int? PreparationTimeMinutes { get; set; }
        protected MealCategory? SelectedMealCategory { get; set; }
        protected DifficultyLvl? SelectedDifficultyLvl { get; set; }
        protected List<MealCategory> MealCategories { get; } = Enum.GetValues<MealCategory>().ToList();
        protected List<DifficultyLvl> DifficultyLevels { get; } = Enum.GetValues<DifficultyLvl>().ToList();

        // Ingredients
        protected List<IngredientOutgoingDto> AvailableIngredients { get; set; } = new();
        protected List<RecipeIngredientRow> IngredientRows { get; set; } = new();
        protected long? NewIngredientId { get; set; }
        protected string NewIngredientQuantity { get; set; } = "";
        protected string NewIngredientUnit { get; set; } = "";


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
                AvailableIngredients = await IngredientApiService.GetAllAsync() ?? new();

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

                IngredientRows = recipe.Ingredients.Select(i => new RecipeIngredientRow
                {
                    IngredientId = i.IngredientId,
                    IngredientName = i.IngredientName,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList();
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
            if (await ForbbitOnlineActionsAsync())
            {
                return;
            }

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
                return;
            }

            var patchDto = new PatchRecipeDto
            {
                RecipeIngredients = IngredientRows.Select(r => new RecipeIngredientDto
                {
                    IngredientId = r.IngredientId,
                    Quantity = r.Quantity,
                }).ToList()
            };
            try
            {
                await RecipeApiService.UpdateRecipeIngredientsAsync(Id, patchDto);
            }
            catch
            {
                Notifications.ShowError("Failed to save ingredients.");
            }

            Notifications.ShowSuccess("Recipe updated successfully!");
            await Task.Delay(1000);
            Navigation.NavigateTo($"/recipes");

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

        protected void AddIngredient()
        {
            if (NewIngredientId == null) return;
            var ing = AvailableIngredients.FirstOrDefault(i => i.Id == NewIngredientId.Value);
            if (ing == null) return;

            if (IngredientRows.Any(r => r.IngredientId == NewIngredientId.Value))
            {
                Notifications.ShowError("This ingredient is already added.");
                return;
            }

            IngredientRows.Add(new RecipeIngredientRow
            {
                IngredientId = ing.Id,
                IngredientName = ing.Name,
                Quantity = decimal.TryParse(NewIngredientQuantity, out var q) ? q : 0,
            });

            NewIngredientId = null;
            NewIngredientQuantity = "";
        }

        protected void SetNewIngredient(ChangeEventArgs e)
        {
            if (long.TryParse(e.Value?.ToString(), out var id))
                NewIngredientId = id;
            else
                NewIngredientId = null;
        }

        protected void RemoveIngredient(long ingredientId)
        {
            IngredientRows.RemoveAll(r => r.IngredientId == ingredientId);
        }
    }
}