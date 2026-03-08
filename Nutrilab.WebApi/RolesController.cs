using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Roles;
using Nutrilab.Services;

namespace Nutrilab.WebApi
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolesController(IRoleService roleService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Maintainer")]
        public async Task<ActionResult<List<RoleOutgoingDto>>> GetAll()
        {
            var roles = await roleService.GetAllAsync();
            var result = mapper.Map<List<RoleOutgoingDto>>(roles);
            return Ok(result);
        }

        [HttpPut("{id}/permissions")]
        public async Task<ActionResult> UpdatePermissions(int id, [FromBody] UpdateRolePermissionsDto request)
        {
            await roleService.UpdatePermissionsAsync(id, request);
            return NoContent();
        }
    }
}
