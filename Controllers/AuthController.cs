using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyShop1.Data;
using SkyShop1.DTO;
using SkyShop1.Entities;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SkyShop1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<MakeAuthDTO>> MakeLogin([FromBody] MakeAuthDTO makeAuthDTO)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == makeAuthDTO.Email);

            if (user == null)
            {
                return NotFound("Usuário inexistente.");
            }
            if (user.Password != makeAuthDTO.Password)
            {
                return BadRequest("Email/Senha incorreta.");
            }

            var key = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenGerado = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenGerado,
                User = new
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                }
            });
        }
    }
}
