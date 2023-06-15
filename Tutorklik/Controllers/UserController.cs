using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using Tutorklik.Data;
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

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if ( userDb == null)
            {
                return BadRequest("Not found user");
            }
            return Ok((UserDto)userDb);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> ChangeUser(UserDto user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (userDb == null)
            {
                return BadRequest("Not found user");
            }
            userDb.UserName = user.UserName;
            userDb.Email = user.Email;
            userDb.ProfileImage = user.ProfileImage;
            await _context.SaveChangesAsync();
            return Ok(userDb);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteUser(int userId)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            _context.Users.Remove(userDb);
            // do not know is it good
            await _context.SaveChangesAsync();
            // think what function should return
            return Ok(userId);
        }
    }
}
