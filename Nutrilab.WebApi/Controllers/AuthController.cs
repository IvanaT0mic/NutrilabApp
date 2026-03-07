using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Auths;
using Nutrilab.Services.AuthServices;
using Nutrilab.Services.AuthServices.Models;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginDto request)
        {
            var response = await authService.LoginAsync(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<long>> Register([FromBody] LoginDto request)
        {
            long id = await authService.RegisterUserAsync(request);
            return Ok(id);
        }
    }
}
