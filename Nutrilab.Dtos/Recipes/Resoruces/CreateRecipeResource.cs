using Microsoft.AspNetCore.Http;

namespace Nutrilab.Dtos.Recipes.Resoruces
{
    public record CreateRecipeResource
    {
        public IFormFile File { get; init; }
    }
}
