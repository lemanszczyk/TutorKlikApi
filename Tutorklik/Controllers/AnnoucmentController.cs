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
    public class AnnoucmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AnnoucmentController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // zbudować nowego User w bazie który nie posiada hasła tylko id
        // ewentulnie wewnątch zbudować

        [HttpGet]
        public async Task<ActionResult<List<Annoucement>>> GetAnnoucement()
        {

            return Ok(await _context.Annoucements.Include(x => x.Author).Include(x=>x.Comments).ToListAsync());
        }   
        //[HttpPost("register")]
        //public async Task<ActionResult<List<User>>> Register(UserRegisterDto request)
        //{
        //    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        //    if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName) == null)
        //    {
        //        return BadRequest("User with this username is exist");
        //    }
        //    _context.Users.Add(new User()
        //    {
        //        UserName = request.UserName,
        //        PasswordHash = passwordHash,
        //        Email = request.Email,
        //        UserType = request.UserType
        //    });

        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Users.ToListAsync());
        //}
    }
}
