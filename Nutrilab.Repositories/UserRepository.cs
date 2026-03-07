using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;
using Nutrilab.DataAccess.Models.Users;

namespace Nutrilab.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailWithRolesAndPermissionsAsync(string email);
        Task<bool> DoesUserAlreadyExists(string email);
        Task<User> InsertAsync(User user);
    }

    public sealed class UserRepository(EntityContext context) : BaseRepository<User>(context), IUserRepository
    {
        public Task<bool> DoesUserAlreadyExists(string email)
        {
            return GetQueryable()
                .Where(x => x.Email == email)
                .AnyAsync();
        }

        public Task<User?> GetByEmailWithRolesAndPermissionsAsync(string email)
        {
            //@Tamara po konvenciji se u EF radi == za upite nad bazom jer se prevode u = svakako
            return GetQueryable()
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .ThenInclude(x => x.Permission)
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
