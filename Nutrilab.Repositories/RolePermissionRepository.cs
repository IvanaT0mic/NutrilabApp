using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.RolePermissions;

namespace Nutrilab.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermission>> GetByRoleIdAsync(int roleId);
        Task DeleteRangeAsync(List<RolePermission> rolePermissions);
        Task<List<RolePermission>> InsertRangeAsync(List<RolePermission> data);
    }

    public sealed class RolePermissionRepository(EntityContext context)
        : BaseRepository<RolePermission>(context), IRolePermissionRepository
    {
        public Task<List<RolePermission>> GetByRoleIdAsync(int roleId)
        {
            return GetQueryable().Where(x => x.RoleId == roleId).ToListAsync();
        }
    }
}
