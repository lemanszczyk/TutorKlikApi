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

namespace Tutorklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AnnouncementController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetAnnouncements")]
        public async Task<ActionResult<List<AnnouncementDto>>> GetAnnoucements()
        {
            var listOfAnnoucments = await _context.Annoucements.Include(x => x.Author).Include(x => x.Comments).ToListAsync();   
            return Ok(listOfAnnoucments.Select(x => (AnnouncementDto)x).ToList());
        }
        [HttpGet("GetAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> GetAnnoucements(int id)
        {
            var announcement = _context.Annoucements.Include(x  => x.Author).Include(x => x.Comments).FirstOrDefault(x => x.AnnouncementId == id);
            if (announcement == null)
            {
                return BadRequest("Announcement with this id is not found");
            }
            return (AnnouncementDto)announcement;
        }
    }
}
