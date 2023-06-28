using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using Tutorklik.Data;
using Tutorklik.Models;
using Tutorklik.Models.ModelsDto;

namespace Tutorklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UserController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetUser"), Authorize]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (userDb == null)
            {
                return BadRequest("Not found user");
            }
            return Ok((UserDto)userDb);
        }

        [HttpPost("EditUser"), Authorize]
        public async Task<ActionResult<UserDto>> EditUser(UserDto user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userJWT = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (userDb == null)
            {
                return BadRequest("Not found user");
            }

            if (userJWT != userDb)
            {
                return BadRequest("Only owner of this account can change its data");
            }
            userDb.UserName = user.UserName;
            userDb.Email = user.Email;
            userDb.ProfileImage = user.ProfileImage;
            await _context.SaveChangesAsync();
            return Ok(userDb);
        }

        [HttpDelete("DeleteUser"), Authorize]
        public async Task<ActionResult<int>> DeleteUser(int userId)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userJWT = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (userDb == null)
            {
                return BadRequest("Not found user");
            }

            if (userJWT != userDb)
            {
                return BadRequest("Only owner of this account can change its data");
            }

            _context.Users.Remove(userDb);
            // do not know is it good
            await _context.SaveChangesAsync();
            // think what function should return
            return Ok(userId);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<User>>> UpdateUser(UserDto user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (dbUser == null)
            {
                return BadRequest("User with this id not found for update");
            }
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.UserType = user.UserType;
            dbUser.ProfileImage = user.ProfileImage;
            await _context.SaveChangesAsync();
            return Ok((UserDto)dbUser);
        }

        [HttpPut("UpdatePassword"), Authorize]
        public async Task<ActionResult<List<User>>> UpdatePassword(UserPasswordDto user)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (dbUser == null)
            {
                return BadRequest("Can't update password for non-existing user!");
            }

            dbUser.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
