using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.ShoppingList.CreateShoppingListDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListDetailOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.ShoppingListOutgoingDtos;
using Nutrilab.Dtos.ShoppingList.UpdateShoppingListDtos;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ShoppingListsController(
         IShoppingListService shoppingListService,
         IMapper mapper) : ControllerBase
    {
        // GET /shoppinglists
        [HttpGet]
        public async Task<ActionResult<List<ShoppingListOutgoingDto>>> GetAllAsync()
        {
            var lists = await shoppingListService.GetAllAsync();
            var result = mapper.Map<List<ShoppingListOutgoingDto>>(lists);
            return Ok(result);
        }

        // GET /shoppinglists/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDetailOutgoingDto>> GetByIdAsync([FromRoute] long id)
        {
            var list = await shoppingListService.GetByIdAsync(id);
            var result = mapper.Map<ShoppingListDetailOutgoingDto>(list);
            return Ok(result);
        }

        // POST /shoppinglists
        [HttpPost]
        public async Task<ActionResult<long>> CreateAsync([FromBody] CreateShoppingListDto request)
        {
            var id = await shoppingListService.CreateAsync(request);
            return Ok(id);
        }

        // PUT /shoppinglists/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateShoppingListDto request)
        {
            await shoppingListService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE /shoppinglists/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] long id)
        {
            await shoppingListService.DeleteAsync(id);
            return NoContent();
        }

        // POST /shoppinglists/{id}/items
        [HttpPost("{id}/items")]
        public async Task<ActionResult<ShoppingListItemOutgoingDto>> AddItemAsync(
            [FromRoute] long id,
            [FromBody] CreateShoppingListItemDto request)
        {
            var item = await shoppingListService.AddItemAsync(id, request);
            var result = mapper.Map<ShoppingListItemOutgoingDto>(item);
            return Ok(result);
        }

        // PUT /shoppinglists/{id}/items/{itemId}
        [HttpPut("{id}/items/{itemId}")]
        public async Task<ActionResult> UpdateItemAsync(
            [FromRoute] long id,
            [FromRoute] long itemId,
            [FromBody] UpdateShoppingListItemDto request)
        {
            await shoppingListService.UpdateItemAsync(id, itemId, request);
            return NoContent();
        }

        // DELETE /shoppinglists/{id}/items/{itemId}
        [HttpDelete("{id}/items/{itemId}")]
        public async Task<ActionResult> DeleteItemAsync(
            [FromRoute] long id,
            [FromRoute] long itemId)
        {
            await shoppingListService.DeleteItemAsync(id, itemId);
            return NoContent();
        }
    }
}
