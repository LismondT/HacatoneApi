using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alabuga_API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace Alabuga_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly AlabugaContext _context = new AlabugaContext();
        private readonly JwtService jwtService = new JwtService();

        [HttpPost]
        public async Task<DTOResponse<string>> SignIn([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.EMail == user.EMail && u.Password == user.Password);
            if (existingUser == null)
                return new DTOResponse<string>(StatusCodes.Status401Unauthorized.ToString(), null);

            var token = jwtService.GenerateToken(existingUser);
            return new DTOResponse<string>(StatusCodes.Status200OK.ToString(), token);
        }
    }
}
