using Alabuga_API.Services.Interfaces;

namespace Alabuga_API.Middleware;

public class JwtCookieMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
    {
        // Проверяем cookies на наличие токена
        if (context.Request.Cookies.TryGetValue("auth-token", out var token))
        {
            // Если токен есть в cookies, добавляем его в заголовок Authorization
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Request.Headers.Append("Authorization", $"Bearer {token}");
            }
        }

        await _next(context);
    }
}