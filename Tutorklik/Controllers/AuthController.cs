using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tutorklik.Data;
using Tutorklik.Models;

namespace Tutorklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<List<User>>> Register(UserRegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName) == null)
            {
                return BadRequest("User with this username is exist");
            }
            _context.Users.Add(new User()
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                Email = request.Email,
                UserType = request.UserType
            });

            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("login")]
        public async Task<ActionResult<List<User>>> Login(UserLoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (user == null)
            {
                return BadRequest("User not found!");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password!");
            }

            string token = CreateToken(user);
            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
