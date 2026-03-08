using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Recipes.CreateRecipeDtos;
using Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos;
using Nutrilab.Dtos.Recipes.RecipeOutgoingDto;
using Nutrilab.Dtos.Recipes.UpdateRecipeDtos;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RecipesController(
        IRecipeService recipeService,
        IRecipeIngredientService recipeIngredientService,
        IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<RecipeOutgoingDto>>> GetAllAsync()
        {
            var response = await recipeService.GetAllAsync();
            var result = mapper.Map<List<RecipeOutgoingDto>>(response);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailOutgoingDto>> GetByIdAsync([FromRoute] long id)
        {
            var recipe = await recipeService.GetByIdAsync(id);
            var result = mapper.Map<RecipeDetailOutgoingDto>(recipe);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult<long>> CreateAsync([FromBody] CreateRecipeDto request)
        {
            var id = await recipeService.CreateAsync(request);
            return Ok(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateRecipeDto request)
        {
            await recipeService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpPatch("/{id}")]
        public async Task<ActionResult> UpdateRecipeIngredientDetailsAsync([FromRoute] long id, [FromBody] PatchRecipeDto updateRecipeDto)
        {
            await recipeIngredientService.UpateRecipeIngreditents(id, updateRecipeDto.RecipeIngredients);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult> DeleteAsync([FromRoute] long id)
        {
            await recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
