using System.Security.Claims;
using Alabuga_API.Models;
using Alabuga_API.Models.Auth;
using Alabuga_API.Models.User;

namespace Alabuga_API.Services.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    RefreshToken GenerateRefreshToken();
}