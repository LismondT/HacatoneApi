using Microsoft.AspNetCore.Mvc;
using Alabuga_API.Contracts.Auth;
using Alabuga_API.Models.User;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Alabuga_API.Services.Interfaces;


namespace Alabuga_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserRepository userRepository, IJwtService jwtService) : ControllerBase
    {
        // private readonly AlabugaContext _context = new AlabugaContext();
        // private readonly JwtService _jwtService = new JwtService();

        // На фронте у нас нет авторизации по email и паролю,
        // вход через кнопку "Вход через Алабуга",
        // для упрощения берётся рандомный пользователь 
        [HttpPost]
        public async Task<IActionResult> SignIn()
        {
            //var existingUser = _context.Users.FirstOrDefault(u => u.EMail == user.EMail && u.Password == user.Password);
            User? existingUser = await userRepository.GetRandom();
            if (existingUser == null)
                return Unauthorized();
            //new DTOResponse<string>(StatusCodes.Status401Unauthorized.ToString(), null);

            string accessToken = jwtService.GenerateAccessToken(existingUser);
            SignInResponse response = new(
                AccessToken: accessToken
            );

            return Ok(response);
        }
    }
}