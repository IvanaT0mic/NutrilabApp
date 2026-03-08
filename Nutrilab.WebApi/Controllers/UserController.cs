using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Users;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Maintainer")]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            var result = await userService.GetAllAsync();
            var response = mapper.Map<List<UserDto>>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(long id)
        {
            var user = await userService.GetByIdAsync(id);
            var response = mapper.Map<UserDto>(user);
            return Ok(response);
        }

        [HttpPut("{id}/roles")]
        public async Task<ActionResult> UpdateRoles(long id, [FromBody] UpdateUserRolesDto request)
        {
            await userService.UpdateRolesAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(long id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
