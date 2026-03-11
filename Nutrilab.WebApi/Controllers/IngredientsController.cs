using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Ingredients;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IngredientsController(IIngredientService service, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<IngredientOutgoingDto>>> GetAllAsync()
        {
            var ingredients = await service.GetAllAsync();
            var result = mapper.Map<List<IngredientOutgoingDto>>(ingredients);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientOutgoingDto>> GetByIdAsync([FromRoute] long id)
        {
            var ingredient = await service.GetByIdAsync(id);
            var result = mapper.Map<IngredientOutgoingDto>(ingredient);
            return Ok(result);
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
