using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrilab.Dtos.Permissions;
using Nutrilab.Dtos.Roles;
using Nutrilab.Services;

namespace Nutrilab.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Maintainer")]
    public class RoleController(RoleService roleService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var roles = await roleService.GetAllAsync();
            var result = mapper.Map<List<RoleDto>>(roles);
            return Ok(result);
        }

        [HttpGet("permissions")]
        public async Task<ActionResult<List<PermissionDto>>> GetAllPermissions()
        {
            var permissions = await roleService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpPut("{id}/permissions")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdatePermissions(int id, [FromBody] UpdateRolePermissionsDto request)
        {
            await roleService.UpdatePermissionsAsync(id, request);
            return NoContent();
        }
    }
}
