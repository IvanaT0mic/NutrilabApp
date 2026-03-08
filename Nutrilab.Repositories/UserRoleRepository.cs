using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.UserRoles;

namespace Nutrilab.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetByUserIdAsync(long userId);
        Task DeleteRangeAsync(List<UserRole> userRoles);
        Task<List<UserRole>> InsertRangeAsync(List<UserRole> userRoles);
    }

    public sealed class UserRoleRepository(EntityContext context)
        : BaseRepository<UserRole>(context), IUserRoleRepository
    {
        public Task<List<UserRole>> GetByUserIdAsync(long userId)
        {
            return GetQueryable().Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
