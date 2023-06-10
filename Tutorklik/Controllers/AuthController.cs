using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorklik.Data;
using Tutorklik.Models;

namespace Tutorklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthController(DataContext context)
        {
            _context = context;
        }

        public static User user = new User();
        [HttpPost("register")]
        public async Task<ActionResult<List<User>>> Register(UserRegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName))
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
            user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (user == null)
            {
                return BadRequest("User not found!");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password!");
            }

            return Ok(user);
        }

        //private string CreateToken(User user)
        //{

        //}
    }
}
