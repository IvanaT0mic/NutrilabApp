using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Permissions;

namespace Nutrilab.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync();
    }

    public sealed class PermissionRepository(EntityContext context)
        : BaseRepository<Permission>(context), IPermissionRepository
    {
        public Task<List<Permission>> GetAllAsync() =>
            GetQueryable().OrderBy(p => p.Name).ToListAsync();
    }
}
