using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Ingredients;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IngredientsController(IIngredientService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<IngredientOutgoingDto>>> GetAllAsync()
        {
            var ingredients = await service.GetAllAsync();
            var result = new List<IngredientOutgoingDto>();
            foreach (var ingredient in ingredients)
            {
                var item = new IngredientOutgoingDto();
                item.Id = ingredient.Id;
                item.Name = ingredient.Name;
                item.Unit = ingredient.Unit;

                result.Add(item);
            }
            return Ok(result);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<IngredientOutgoingDto>> GetByIdAsync([FromRoute] long id)
        {
            var ingredient = await service.GetByIdAsync(id);
            var item = new IngredientOutgoingDto();
            item.Id = ingredient.Id;
            item.Name = ingredient.Name;
            item.Unit = ingredient.Unit;
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult<long>> CreateAsync([FromBody] CreateIngredientDto request)
        {
            var ingredientId = await service.CreateAsync(request);
            return Ok(ingredientId);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
