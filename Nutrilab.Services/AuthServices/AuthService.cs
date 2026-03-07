using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Dtos.Auths;
using Nutrilab.Repositories;
using Nutrilab.Services.AuthServices.Models;
using Nutrilab.Services.Handlers;
using Nutrilab.Shared.Models;
using Nutrilab.Shared.Models.Exceptions;

namespace Nutrilab.Services.AuthServices
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginDto request);
        Task<long> RegisterUserAsync(LoginDto request);
    }

    public sealed class AuthService(
        IUserRepository userRepo,
        IJwtHandler jwtHandler
        ) : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginDto request)
        {
            var user = await userRepo.GetByEmailWithRolesAndPermissionsAsync(request.Email)
                ?? throw new UnauthorizedException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid credentials");

            var principal = new UserPrincipal
            {
                Id = user.Id,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Permissions = user.UserRoles
                    .SelectMany(ur => ur.Role.RolePermissions)
                    .Select(rp => rp.Permission.Name)
                    .Distinct()
                    .ToList()
            };

            return new LoginResponse
            {
                Access = jwtHandler.GenerateAccessToken(principal),
            };
        }

        public async Task<long> RegisterUserAsync(LoginDto request)
        {
            var alreadyExists = await userRepo.DoesUserAlreadyExists(request.Email);
            if (alreadyExists)
            {
                throw new BadRequestException("User with given email already exists.");
            }

            var newUser = new User()
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var userDb = userRepo.InsertAsync(newUser);
            return userDb.Id;
        }
    }
}
