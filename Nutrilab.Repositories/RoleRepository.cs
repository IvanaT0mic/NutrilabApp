using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Roles;

namespace Nutrilab.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllWithPermissionsAsync();
        Task<Role?> GetByIdWithPermissionsAsync(int id);
    }

    public sealed class RoleRepository(EntityContext context)
        : BaseRepository<Role>(context), IRoleRepository
    {
        public Task<List<Role>> GetAllWithPermissionsAsync() =>
            GetQueryable()
                .Include(x => x.RolePermissions)
                    .ThenInclude(x => x.Permission)
                .AsSplitQuery()
                .ToListAsync();

        public Task<Role?> GetByIdWithPermissionsAsync(int id) =>
            GetQueryable()
                .Include(x => x.RolePermissions)
                    .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
