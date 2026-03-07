using Microsoft.IdentityModel.Tokens;
using Nutrilab.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nutrilab.Services.Handlers
{
    public interface IJwtHandler
    {
        string GenerateAccessToken(UserPrincipal user);
        UserPrincipal ValidateToken(string token);
    }

    public sealed class JwtHandler(JwtSettings _settings) : IJwtHandler
    {
        public string GenerateAccessToken(UserPrincipal user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
            claims.AddRange(user.Permissions.Select(p => new Claim("permission", p)));

            return GenerateToken(claims, _settings.AccessTokenSecret, _settings.AccessTokenExpTime);
        }

        public UserPrincipal ValidateToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AccessTokenSecret));

            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return new UserPrincipal
            {
                Id = long.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
                Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(),
                Permissions = principal.FindAll("permission").Select(c => c.Value).ToList()
            };
        }

        private string GenerateToken(List<Claim> claims, string secret, int expMinutes)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
