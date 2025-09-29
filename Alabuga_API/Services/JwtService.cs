using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alabuga_API.Models.Auth;
using Alabuga_API.Models.User;
using Alabuga_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Alabuga_API.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdmakmcboaragb"));
            var credetianals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim(ClaimTypes.Name, user.Name) };

            var token = new JwtSecurityToken("http://localhost:7048", null, claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credetianals);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
