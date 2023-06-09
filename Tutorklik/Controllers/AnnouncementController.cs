﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            var listOfAnnoucments = await _context.Annoucements.Include(x => x.Author).Include(x => x.Comments).Include("Comments.Author").ToListAsync();
            if (listOfAnnoucments == null)
            {
                return BadRequest("No announcements to display");
            }
            return Ok(listOfAnnoucments.Select(x => (AnnouncementDto)x).ToList());
        }

        [HttpGet("GetAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> GetAnnoucement(int id)
        {
            var announcement = _context.Annoucements.Include(x  => x.Author).Include(x => x.Comments).Include("Comments.Author").FirstOrDefault(x => x.AnnouncementId == id);

            if (announcement == null)
            {
                return BadRequest("Announcement with this id is not found");
            }
            return Ok((AnnouncementDto)announcement);
        }

        [HttpGet("GetUserAnnouncements"), Authorize(Roles = "Tutor")]
        public async Task<ActionResult<List<AnnouncementDto>>> GetUserAnnouncements()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            var listOfAnnoucments = await _context.Annoucements.Where(x => x.Author.UserName == userName).Include(x => x.Author).Include(x => x.Comments).Include("Comments.Author").ToListAsync();
            return Ok(listOfAnnoucments.Select(x => (AnnouncementDto)x).ToList());
        }

        [HttpPost("AddAnnouncement"), Authorize(Roles = "Tutor")]
        public async Task<ActionResult<AnnouncementDto>> AddAnnoucement(AnnouncementDto announcement)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);          

            var newAnnoucement = new Announcement()
            {
                AnnouncementName = announcement.AnnoucementName,
                AnnouncementDescription = announcement.AnnoucementDescription,
                Tags = String.Join( ".", announcement.Tags),
                Author = user!,
            };
            _context.Annoucements.Add(newAnnoucement);
            await _context.SaveChangesAsync();

            return Ok(announcement);
        }

        [HttpPost("EditAnnouncement"), Authorize(Roles = "Tutor")]
        public async Task<ActionResult<AnnouncementDto>> EditAnnouncement(AnnouncementDto announcement)
        {
            var announcementDb = _context.Annoucements.Include(x => x.Author).Include(x => x.Comments).Include("Comments.Author").FirstOrDefault(x => x.AnnouncementId == announcement.AnnoucementId);
            if (announcementDb == null)
            {
                return BadRequest("Announcement with this id is not found");
            }

            announcementDb.AnnouncementName = announcement.AnnoucementName;
            announcementDb.AnnouncementDescription = announcement.AnnoucementDescription;
            announcementDb.Tags = String.Join( ".", announcement.Tags);

            await _context.SaveChangesAsync();
            return Ok(announcementDb);
        }

        [HttpDelete("DeleteAnnouncement"), Authorize]
        public async Task<ActionResult<AnnouncementDto>> DeleteAnnouncement(int id)
        {
            var announcementDb = await _context.Annoucements.FirstOrDefaultAsync(x => x.AnnouncementId == id);

            if (announcementDb == null)
            {
                return BadRequest("There is no announcement");
            }

            _context.Annoucements.Remove(announcementDb);

            await _context.SaveChangesAsync();
            return Ok(announcementDb);
        }
    }
}
