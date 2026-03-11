using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.ShopingLists
{
    public class ShoppingListBase : PageBase
    {
        [Inject] protected ShoppingListApiService ShoppingListApiService { get; set; } = default!;

        protected List<ShoppingListOutgoingDto> Lists { get; set; } = new();
        protected bool IsLoading { get; set; } = true;
        protected bool IsCreating { get; set; } = false;
        protected bool ShowCreateForm { get; set; } = false;
        protected string NewListName { get; set; } = "";

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
                Notifications.ShowError("Failed to load shopping lists.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task CreateList()
        {
            if (await ForbbitOnlineActionsAsync())
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(NewListName)) return;
            IsCreating = true;

            var id = await ShoppingListApiService.CreateAsync(new CreateShoppingListDto { Name = NewListName });
            if (id.HasValue)
            {
                NewListName = "";
                ShowCreateForm = false;
                await LoadLists();
                Notifications.ShowSuccess("Shopping list created!");
                _ = Task.Delay(500).ContinueWith(_ => { InvokeAsync(StateHasChanged); });
            }
            else
            {
                Notifications.ShowError("Failed to create list.");
            }

            IsCreating = false;
        }

        protected async Task DeleteList(long id)
        {
            if (await ForbbitOnlineActionsAsync())
            {
                return;
            }
            var success = await ShoppingListApiService.DeleteAsync(id);
            if (success)
                await LoadLists();
            else
                Notifications.ShowError("Could not delete list.");
        }
    }
}