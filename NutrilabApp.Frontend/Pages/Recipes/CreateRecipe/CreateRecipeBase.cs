using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Nutrilab.Dtos.Ingredients;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Shared.Enums;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Pages.Recipes
{
    public class CreateRecipeBase : ComponentBase
    {
        [Inject] protected RecipeApiService RecipeApiService { get; set; } = default!;
        [Inject] protected IngredientApiService IngredientApiService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;
        protected string? ErrorMessage { get; set; }
        protected string? SuccessMessage { get; set; }

        // Form fields
        protected string Name { get; set; } = "";
        protected string Description { get; set; } = "";
        protected int? PreparationTimeMinutes { get; set; }
        protected MealCategory? SelectedMealCategory { get; set; }
        protected DifficultyLvl? SelectedDifficultyLvl { get; set; }

        // Image upload
        protected IBrowserFile? SelectedImageFile { get; set; }
        protected string? ImagePreviewUrl { get; set; }

        // Ingredient management
        protected List<IngredientOutgoingDto> AvailableIngredients { get; set; } = new();
        protected List<RecipeIngredientRow> IngredientRows { get; set; } = new();
        protected long? NewIngredientId { get; set; }
        protected string NewIngredientQuantity { get; set; } = "";
        protected string NewIngredientUnit { get; set; } = "";

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
            if (!roles.Contains("Admin") && !roles.Contains("Editor"))
            {
                Navigation.NavigateTo("/recipes");
                return;
            }

            try
            {
                AvailableIngredients = await IngredientApiService.GetAllAsync() ?? new();
            }
            catch { }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task HandleImageSelected(InputFileChangeEventArgs e)
        {
            SelectedImageFile = e.File;
            try
            {
                var buffer = new byte[SelectedImageFile.Size];
                await SelectedImageFile.OpenReadStream(5 * 1024 * 1024).ReadAsync(buffer);
                ImagePreviewUrl = $"data:{SelectedImageFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
            }
            catch { ImagePreviewUrl = null; }
        }

        protected void AddIngredient()
        {
            if (NewIngredientId == null) return;
            var ing = AvailableIngredients.FirstOrDefault(i => i.Id == NewIngredientId.Value);
            if (ing == null) return;

            if (IngredientRows.Any(r => r.IngredientId == NewIngredientId.Value))
            {
                ErrorMessage = "This ingredient is already added.";
                return;
            }

            IngredientRows.Add(new RecipeIngredientRow
            {
                IngredientId = ing.Id,
                IngredientName = ing.Name,
                Quantity = decimal.TryParse(NewIngredientQuantity, out var q) ? q : 0,
                Unit = NewIngredientUnit
            });

            NewIngredientId = null;
            NewIngredientQuantity = "";
            NewIngredientUnit = "";
            ErrorMessage = null;
        }

        protected void RemoveIngredient(long ingredientId)
        {
            IngredientRows.RemoveAll(r => r.IngredientId == ingredientId);
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

        protected void SetNewIngredient(ChangeEventArgs e)
        {
            if (long.TryParse(e.Value?.ToString(), out var id))
                NewIngredientId = id;
            else
                NewIngredientId = null;
        }

        protected async Task CreateRecipe()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Recipe name is required.";
                return;
            }

            IsSaving = true;
            ErrorMessage = null;

            var dto = new CreateRecipeDto
            {
                Name = Name,
                Description = string.IsNullOrWhiteSpace(Description) ? null : Description,
                PreparationTimeMinutes = PreparationTimeMinutes,
                MealCategory = SelectedMealCategory,
                DifficultyLvl = SelectedDifficultyLvl,
                Ingredients = IngredientRows.Select(r => new RecipeIngredientDto
                {
                    IngredientId = r.IngredientId,
                    Quantity = r.Quantity,
                }).ToList()
            };

            var newId = await RecipeApiService.CreateRecipeAsync(dto);
            if (newId == null)
            {
                ErrorMessage = "Failed to create recipe.";
                IsSaving = false;
                return;
            }

            // Upload image if selected
            if (SelectedImageFile != null)
            {
                try
                {
                    var stream = SelectedImageFile.OpenReadStream(5 * 1024 * 1024);
                    await RecipeApiService.UploadImageAsync(newId.Value, stream, SelectedImageFile.Name);
                }
                catch { }
            }

            SuccessMessage = "Recipe created!";
            await Task.Delay(800);
            Navigation.NavigateTo($"/recipes/{newId.Value}");
        }

        public class RecipeIngredientRow
        {
            public long IngredientId { get; set; }
            public string IngredientName { get; set; } = "";
            public decimal Quantity { get; set; }
            public string Unit { get; set; } = "";
        }
    }
}