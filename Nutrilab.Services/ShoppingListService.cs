using Nutrilab.DataAccess.Models.ShoppingListItems;
using Nutrilab.DataAccess.Models.ShoppingLists;
using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos;
using Nutrilab.Repositories;
using Nutrilab.Services.Handlers;
using Nutrilab.Shared.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Services
{
    public interface IShoppingListService
    {
        Task<List<ShoppingList>> GetAllAsync();
        Task<ShoppingList> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateShoppingListDto request);
        Task UpdateAsync(long id, UpdateShoppingListDto request);
        Task DeleteAsync(long id);

        Task<ShoppingListItem> AddItemAsync(long shoppingListId, CreateShoppingListItemDto request);
        Task UpdateItemAsync(long shoppingListId, long itemId, UpdateShoppingListItemDto request);
        Task DeleteItemAsync(long shoppingListId, long itemId);
    }

    public sealed class ShoppingListService(
        IShoppingListRepository repo,
        IShoppingListItemRepository itemRepo,
        ICurrentUserService currentUserService
    ) : IShoppingListService
    {
        public async Task<List<ShoppingList>> GetAllAsync()
        {
            var userId = currentUserService.GetCurrentUser().Id;
            return await repo.GetAllByUserIdAsync(userId);
        }

        public async Task<ShoppingList> GetByIdAsync(long id)
        {
            var list = await repo.GetByIdWithItemsAsync(id)
                ?? throw new NotFoundException($"Shopping list {id} not found");

            EnsureOwnership(list.CreatedByUserId);
            return list;
        }

        public async Task<long> CreateAsync(CreateShoppingListDto request)
        {
            var userId = currentUserService.GetCurrentUser().Id;

            var shoppingList = new ShoppingList
            {
                Name = request.Name,
                CreatedByUserId = userId,
                Items = request.Items.Select(i => new ShoppingListItem
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    IsChecked = false
                }).ToList()
            };

            var created = await repo.InsertAsync(shoppingList);
            return created.Id;
        }

        public async Task UpdateAsync(long id, UpdateShoppingListDto request)
        {
            var list = await repo.GetByIdWithItemsAsync(id)
                ?? throw new NotFoundException($"Shopping list {id} not found");

            EnsureOwnership(list.CreatedByUserId);

            list.Name = request.Name;
            await repo.UpdateAsync(list);
        }

        public async Task DeleteAsync(long id)
        {
            var list = await repo.GetByIdWithItemsAsync(id)
                ?? throw new NotFoundException($"Shopping list {id} not found");

            EnsureOwnership(list.CreatedByUserId);
            await repo.DeleteAsync(list);
        }

        public async Task<ShoppingListItem> AddItemAsync(long shoppingListId, CreateShoppingListItemDto request)
        {
            var list = await repo.GetByIdWithItemsAsync(shoppingListId)
                ?? throw new NotFoundException($"Shopping list {shoppingListId} not found");

            EnsureOwnership(list.CreatedByUserId);

            var item = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                Name = request.Name,
                Quantity = request.Quantity,
                Unit = request.Unit,
                IsChecked = false
            };

            return await itemRepo.InsertAsync(item);
        }

        public async Task UpdateItemAsync(long shoppingListId, long itemId, UpdateShoppingListItemDto request)
        {
            var list = await repo.GetByIdWithItemsAsync(shoppingListId)
                ?? throw new NotFoundException($"Shopping list {shoppingListId} not found");

            EnsureOwnership(list.CreatedByUserId);

            var item = await itemRepo.GetByIdAsync(itemId)
                ?? throw new NotFoundException($"Item {itemId} not found");

            if (item.ShoppingListId != shoppingListId)
                throw new BadRequestException("Item does not belong to this shopping list");

            item.Name = request.Name;
            item.Quantity = request.Quantity;
            item.Unit = request.Unit;
            item.IsChecked = request.IsChecked;

            await itemRepo.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(long shoppingListId, long itemId)
        {
            var list = await repo.GetByIdWithItemsAsync(shoppingListId)
                ?? throw new NotFoundException($"Shopping list {shoppingListId} not found");

            EnsureOwnership(list.CreatedByUserId);

            var item = await itemRepo.GetByIdAsync(itemId)
                ?? throw new NotFoundException($"Item {itemId} not found");

            if (item.ShoppingListId != shoppingListId)
                throw new BadRequestException("Item does not belong to this shopping list");

            await itemRepo.DeleteAsync(item);
        }

        private void EnsureOwnership(long ownerId)
        {
            var userId = currentUserService.GetCurrentUser().Id;
            if (ownerId != userId)
                throw new UnauthorizedException("You can only access your own shopping lists");
        }
    }
}
