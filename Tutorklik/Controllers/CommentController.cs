using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Tutorklik.Data;
using Tutorklik.Models;
using Tutorklik.Models.ModelsDto;

namespace Tutorklik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public CommentController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetComment")]
        public async Task<ActionResult<CommentDto>> GetComment(int id)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            
            if (commentDb == null)
            {
                return BadRequest("There is no comments in this announcement");
            }

            return Ok(commentDb);
        }

        [HttpPost("AddComment"), Authorize]
        public async Task<ActionResult<CommentDto>> AddComment(CommentDto comment)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);

            var newComment = new Comment()
            {
                Description = comment.Description,
                Rate = comment.Rate,
                Author = user!,
                AnnouncementId = comment.AnnouncementId,
            };
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        [HttpPost("EditComment"), Authorize]
        public async Task<ActionResult<CommentDto>> EditComment(CommentDto comment)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == comment.CommentId);

            if (commentDb == null)
            {
                return BadRequest("There is no comment with this id");
            }

            commentDb.Description = comment.Description;
            commentDb.Rate = comment.Rate;

            await _context.SaveChangesAsync();
            return Ok(commentDb);
        }

        [HttpDelete("DeleteComment"), Authorize]
        public async Task<ActionResult<CommentDto>> DeleteComment(int id)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (commentDb == null)
            {
                return BadRequest("There is no comments in this announcement");
            }

            _context.Comments.Remove(commentDb);
            await _context.SaveChangesAsync();
            return Ok(commentDb);
        }
    }
}
