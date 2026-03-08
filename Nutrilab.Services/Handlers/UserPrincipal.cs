using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nutrilab.Shared.Models;
using System.Security.Claims;

namespace Nutrilab.Services.Handlers
{
    public interface ICurrentUserService
    {
        IUserPrincipal? GetCurrentUser();
    }

    public class CurrentUserService(
        IHttpContextAccessor http,
        ILogger<ICurrentUserService> logger) : ICurrentUserService
    {
        private UserPrincipal? _cachedUser;

        public IUserPrincipal? GetCurrentUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            try
            {
                var claims = http.HttpContext?.User.Claims.ToList();
                if (claims == null || !claims.Any())
                    return null;

                var userIdString = claims
                    .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(userIdString))
                    return null;

                var email = claims
                    .SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";

                var roles = claims
                    .Where(x => x.Type == ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToList();

                var permissions = claims
                    .Where(x => x.Type == "permission")
                    .Select(x => x.Value)
                    .ToList();

                _cachedUser = new UserPrincipal
                {
                    Id = long.Parse(userIdString),
                    Email = email,
                    Roles = roles,
                    Permissions = permissions
                };

                return _cachedUser;
            }
            catch (Exception ex)
            {
                logger.LogError("Cannot get current user from context. {message}", ex.Message);
                return null;
            }
        }
    }
}
