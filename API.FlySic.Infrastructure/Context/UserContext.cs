using API.FlySic.Domain.Interfaces.Context;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.FlySic.Infrastructure.Context
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid GetUserId()
        {
            var sub = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(sub, out var id);
            return id;
        }

        public string? GetEmail()
        {
            return User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
        }

        public bool IsFirstAccess()
        {
            var value = User?.FindFirst("IsFirstAccess")?.Value;
            return value != null && bool.TryParse(value, out var result) && result;
        }
    }
}
