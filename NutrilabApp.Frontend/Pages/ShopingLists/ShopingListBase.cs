using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.ShoppingLists
{
    public class ShoppingListBase : ComponentBase
    {
        [Inject] protected ShoppingListApiService ShoppingListApiService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected List<ShoppingListOutgoingDto> Lists { get; set; } = new();
        protected bool IsLoading { get; set; } = true;
        protected bool IsCreating { get; set; } = false;
        protected bool ShowCreateForm { get; set; } = false;
        protected string NewListName { get; set; } = "";
        protected string? ErrorMessage { get; set; }
        protected string? SuccessMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            await LoadLists();
        }

        protected async Task LoadLists()
        {
            IsLoading = true;
            try
            {
                Lists = await ShoppingListApiService.GetAllAsync() ?? new();
            }
            catch
            {
                ErrorMessage = "Failed to load shopping lists.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task CreateList()
        {
            if (string.IsNullOrWhiteSpace(NewListName)) return;
            IsCreating = true;
            ErrorMessage = null;

            var id = await ShoppingListApiService.CreateAsync(new CreateShoppingListDto { Name = NewListName });
            if (id.HasValue)
            {
                NewListName = "";
                ShowCreateForm = false;
                await LoadLists();
                SuccessMessage = "Shopping list created!";
                _ = Task.Delay(2000).ContinueWith(_ => { SuccessMessage = null; InvokeAsync(StateHasChanged); });
            }
            else
            {
                ErrorMessage = "Failed to create list.";
            }

            IsCreating = false;
        }

        protected async Task DeleteList(long id)
        {
            var success = await ShoppingListApiService.DeleteAsync(id);
            if (success)
                await LoadLists();
            else
                ErrorMessage = "Could not delete list.";
        }
    }
}