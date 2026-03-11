using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.ShopingLists
{
    public class ShoppingListDetailBase : PageBase
    {
        [Inject] protected ShoppingListApiService ShoppingListApiService { get; set; } = default!;

        [Parameter] public long Id { get; set; }

        protected ShoppingListDetailOutgoingDto? List { get; set; }
        protected bool IsLoading { get; set; } = true;

        // Add item form
        protected bool ShowAddForm { get; set; } = false;
        protected string NewItemName { get; set; } = "";
        protected string NewItemQuantity { get; set; } = "1";
        protected string NewItemUnit { get; set; } = "";
        protected bool IsAddingItem { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            await LoadList();
        }

        protected async Task LoadList()
        {
            IsLoading = true;
            try
            {
                List = await ShoppingListApiService.GetByIdAsync(Id);
            }
            catch
            {
                Notifications.ShowError("Failed to load shopping list.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task ToggleItem(ShoppingListItemOutgoingDto item)
        {
            var dto = new UpdateShoppingListItemDto
            {
                Name = item.Name,
                Quantity = item.Quantity,
                Unit = item.Unit,
                IsChecked = !item.IsChecked
            };

            var success = await ShoppingListApiService.UpdateItemAsync(Id, item.Id, dto);
            if (success)
            {
                item.IsChecked = !item.IsChecked;
                StateHasChanged();
            }
        }

        protected async Task AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewItemName)) return;
            IsAddingItem = true;

            var dto = new CreateShoppingListItemDto
            {
                Name = NewItemName,
                Quantity = decimal.TryParse(NewItemQuantity, out var q) ? q : 1,
                Unit = NewItemUnit
            };

            var newItem = await ShoppingListApiService.AddItemAsync(Id, dto);
            if (newItem != null)
            {
                List?.Items.Add(newItem);
                NewItemName = "";
                NewItemQuantity = "1";
                NewItemUnit = "";
                ShowAddForm = false;
            }
            else
            {
                Notifications.ShowError("Failed to add item.");
            }

            IsAddingItem = false;
        }

        protected async Task DeleteItem(long itemId)
        {
            var success = await ShoppingListApiService.DeleteItemAsync(Id, itemId);
            if (success && List != null)
            {
                List.Items.RemoveAll(i => i.Id == itemId);
                StateHasChanged();
            }
        }

        protected int CheckedCount => List?.Items.Count(i => i.IsChecked) ?? 0;
        protected int TotalCount => List?.Items.Count ?? 0;
        protected double Progress => TotalCount == 0 ? 0 : (double)CheckedCount / TotalCount * 100;
    }
}