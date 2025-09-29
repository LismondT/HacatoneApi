using Alabuga_API.Models;
using Alabuga_API.Models.Auth;
using Alabuga_API.Models.User;

namespace Alabuga_API.Services.Interfaces;

public interface IJwtService
{
    public string GenerateAccessToken(User user);
    public RefreshToken GenerateRefreshToken();
}