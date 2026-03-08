using Nutrilab.DataAccess.Models.RolePermissions;
using Nutrilab.Dtos.Roles;
using Nutrilab.Repositories;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Models.Exceptions;
using System.Transactions;

namespace Nutrilab.Services
{
    public interface IRoleService
    {
        Task<List<IRole>> GetAllAsync();
        Task UpdatePermissionsAsync(int id, UpdateRolePermissionsDto request);
    }

    public sealed class RoleService(
            IRoleRepository roleRepo,
            IRolePermissionRepository rolePermissionRepo) : IRoleService
    {
        public async Task<List<IRole>> GetAllAsync()
        {
            var roles = await roleRepo.GetAllWithPermissionsAsync();
            return roles.ToList<IRole>();
        }

        public async Task UpdatePermissionsAsync(int id, UpdateRolePermissionsDto request)
        {
            var role = await roleRepo.GetByIdWithPermissionsAsync(id);
            if (role == null)
            {
                throw new NotFoundException($"Role {id} not found");
            }

            var existing = await rolePermissionRepo.GetByRoleIdAsync(id);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (existing.Count != 0)
                await rolePermissionRepo.DeleteRangeAsync(existing);

            if (request.PermissionIds.Count != 0)
            {
                var newPermissions = request.PermissionIds.Select(permId => new RolePermission
                {
                    RoleId = id,
                    PermissionId = permId
                }).ToList();
                await rolePermissionRepo.InsertRangeAsync(newPermissions);
            }

            scope.Complete();
        }
    }
}
