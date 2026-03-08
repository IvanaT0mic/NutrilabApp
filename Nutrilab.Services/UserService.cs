using Nutrilab.DataAccess.Models.UserRoles;
using Nutrilab.Dtos.Users;
using Nutrilab.Repositories;
using Nutrilab.Services.Handlers;
using Nutrilab.Shared.Interfaces.EntityModels;
using Nutrilab.Shared.Models.Exceptions;
using System.Transactions;

namespace Nutrilab.Services
{
    public interface IUserService
    {
        Task<List<IUser>> GetAllAsync();
        Task<IUser> GetByIdAsync(long id);
        Task UpdateRolesAsync(long id, UpdateUserRolesDto request);
        Task DeleteAsync(long id);
    }


    public sealed class UserService(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IFavouriteRecipeRepository favouriteRecipeRepository,
        IRecipeRepository recipeRepository,
        ICurrentUserService currentUser) : IUserService
    {
        public async Task DeleteAsync(long id)
        {
            var current = currentUser.GetCurrentUser();
            if (current?.Id == id)
            {
                throw new BadRequestException("You cannot delete your own account");
            }

            var user = await userRepository.GetByIdWithRolesAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User {id} not found");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var userRoles = await userRoleRepository.GetByUserIdAsync(id);
            if (userRoles.Count != 0)
                await userRoleRepository.DeleteRangeAsync(userRoles);

            var favourites = await favouriteRecipeRepository.GetByUserIdAsync(id);
            if (favourites.Count != 0)
                await favouriteRecipeRepository.DeleteRangeAsync(favourites);

            var recepies = await recipeRepository.AnyWithUserIdAsync(id);
            if (recepies)
            {
                throw new BadRequestException("You cannot delete user because of related recepies");
            }

            await userRepository.DeleteAsync(user);

            scope.Complete();
        }

        public async Task<List<IUser>> GetAllAsync()
        {
            var result = await userRepository.GetAllAsync();
            return result.ToList<IUser>();
        }

        public async Task<IUser> GetByIdAsync(long id)
        {
            var user = await userRepository.GetByIdWithRolesAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User {id} not found");
            }
            return user;
        }

        public async Task UpdateRolesAsync(long id, UpdateUserRolesDto request)
        {
            var user = await userRepository.GetByIdWithRolesAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User {id} not found");
            }

            var existing = await userRoleRepository.GetByUserIdAsync(id);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (existing.Count != 0)
            {
                await userRoleRepository.DeleteRangeAsync(existing);
            }

            if (request.RoleIds.Count != 0)
            {
                var newRoles = request.RoleIds.Select(roleId =>
                    new UserRole
                    {
                        UserId = id,
                        RoleId = roleId
                    }
                    ).ToList();

                await userRoleRepository.InsertRangeAsync(newRoles);
            }

            scope.Complete();
        }
    }
}
