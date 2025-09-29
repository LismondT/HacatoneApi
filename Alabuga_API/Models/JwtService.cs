using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Alabuga_API.Models
{
    public class JwtService
    {
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdmakmcboaragb"));
            var credetianals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim(ClaimTypes.Name, user.Name) };

            var token = new JwtSecurityToken("http://localhost:7048", null, claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credetianals);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
