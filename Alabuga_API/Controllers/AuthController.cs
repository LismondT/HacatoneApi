using Microsoft.AspNetCore.Mvc;
using Alabuga_API.Contracts.Auth;
using Alabuga_API.Models.User;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Alabuga_API.Services.Interfaces;
using System.Security.Claims;
using Alabuga_API.Persistens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Alabuga_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserRepository userRepository, IJwtService jwtService, AlabugaContext context) : ControllerBase
{
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn()
    {
        User? existingUser = await userRepository.GetRandom();
        if (existingUser == null)
            return Unauthorized("User not found");

        string accessToken = jwtService.GenerateAccessToken(existingUser);
            
        Response.Cookies.Append("auth-token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.AddMinutes(15)
        });

        var response = new SignInResponse(AccessToken: accessToken);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        // Получаем ID пользователя из claims
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            // Если нет UserId, пробуем получить по имени
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
                return Unauthorized("Invalid token");

            var userByName = await context.Users
                .Include(u => u.FkRankNavigation)
                .FirstOrDefaultAsync(u => u.Name == userName);
                
            if (userByName == null)
                return Unauthorized("User not found");

            return await CreateProfileResponse(userByName);
        }

        // Получаем пользователя по ID из базы данных
        var user = await context.Users
            .Include(u => u.FkRankNavigation)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return Unauthorized("User not found");

        return await CreateProfileResponse(user);
    }

    private async Task<IActionResult> CreateProfileResponse(User user)
    {
        var profileResponse = new ProfileResponse(
            Image: user.Photo ?? "default-avatar.png",
            FirstName: user.FirstName ?? "Не указано",
            LastName: user.Name ?? "Не указано",
            Surname: user.Patronymic ?? "Не указано",
            Exp: user.Expirience ?? 0,
            Energy: user.Energy ?? 100,
            RankName: user.FkRankNavigation?.Name ?? "Новичок",
            Email: user.EMail ?? "Не указано",
            Phone: user.Phone ?? "Не указано",
            Direction: user.Direction ?? "Не указано"
        );

        return Ok(profileResponse);
    }

    [HttpPost("signout")]
    public IActionResult SignOut()
    {
        Response.Cookies.Delete("auth-token");
        return Ok(new { message = "Signed out successfully" });
    }
        
    [HttpGet("profile-from-cookie")]
    public async Task<IActionResult> GetProfileFromCookie()
    {
        if (Request.Cookies.TryGetValue("auth-token", out var token))
        {
            var principal = jwtService.ValidateToken(token);
            if (principal != null)
            {
                var userIdClaim = principal.FindFirst("UserId")?.Value;
                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                {
                    var user = await context.Users
                        .Include(u => u.FkRankNavigation)
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (user != null)
                    {
                        return await CreateProfileResponse(user);
                    }
                }
            }
        }

        return Unauthorized("Invalid or expired token");
    }
}