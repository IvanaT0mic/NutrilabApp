using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RecipesController(IRecipeService service) : ControllerBase
    {
    }
}
