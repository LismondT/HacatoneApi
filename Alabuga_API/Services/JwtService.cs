using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Alabuga_API.Models.Auth;
using Alabuga_API.Models.User;
using Alabuga_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Alabuga_API.Services;

public class JwtService : IJwtService
{
    private readonly SymmetricSecurityKey _securityKey = new("asdwafatwasdmakmcboaragbвфыввыфвфывф"u8.ToArray());

    public string GenerateAccessToken(User user)
    {
        var claims = new[] 
        { 
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.EMail),
            new Claim("UserId", user.Id.ToString()),
            new Claim("FirstName", user.FirstName ?? ""),
            new Claim("LastName", user.Name ?? "")
        };

        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:7048",
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
                
            if (!tokenHandler.CanReadToken(token))
                return null;

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:7048",
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
            
        return new RefreshToken(
            Token: Convert.ToBase64String(randomNumber),
            Expires: DateTime.UtcNow.AddDays(7)
        );
    }
}