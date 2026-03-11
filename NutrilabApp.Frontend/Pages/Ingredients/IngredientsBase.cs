using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Ingredients;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.Ingredients
{
    public class IngredientsBase : PageBase
    {
        [Inject] protected IngredientApiService IngredientApiService { get; set; } = default!;

        protected List<IngredientOutgoingDto> Ingredients { get; set; } = new();
        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;
        protected bool IsAdmin { get; set; } = false;


        // Search
        protected string SearchQuery { get; set; } = "";

        // Inline add
        protected bool IsAdding { get; set; } = false;
        protected string NewName { get; set; } = "";
        protected string NewUnit { get; set; } = "";

        protected List<IngredientOutgoingDto> FilteredIngredients =>
            string.IsNullOrWhiteSpace(SearchQuery)
                ? Ingredients
                : Ingredients.Where(i => i.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

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
                Navigation.NavigateTo("/dashboard");
                return;
            }

            IsAdmin = roles.Contains("Admin");
            await LoadIngredients();
        }

        private async Task LoadIngredients()
        {
            IsLoading = true;
            try
            {
                Ingredients = await IngredientApiService.GetAllAsync() ?? new();
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected void StartAdding()
        {
            IsAdding = true;
            NewName = "";
            NewUnit = "";
        }

        protected void CancelAdding()
        {
            IsAdding = false;
            NewName = "";
            NewUnit = "";
        }

        protected async Task SaveNewIngredient()
        {
            if (string.IsNullOrWhiteSpace(NewName))
            {
                Notifications.ShowError("Ingredient name is required");
                return;
            }

            IsSaving = true;

            try
            {
                await ActionGuard.GuardAsync();
                var dto = new CreateIngredientDto { Name = NewName.Trim(), Unit = NewUnit.Trim() };
                var id = await IngredientApiService.CreateAsync(dto);
                if (id != null)
                {
                    Notifications.ShowSuccess($"Ingredient '{NewName}' added!");
                    IsAdding = false;
                    NewName = "";
                    NewUnit = "";
                    await LoadIngredients();
                }
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex.Message);
            }
            finally
            {
                IsSaving = false;
            }
        }
    }
}