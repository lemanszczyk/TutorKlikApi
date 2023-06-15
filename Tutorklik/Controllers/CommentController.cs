using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorklik.Data;
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

        [HttpPost("EditComment")]
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

        [HttpDelete("DeleteComment")]
        public async Task<ActionResult<CommentDto>> DeleteComment(int id)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (commentDb == null)
            {
                return BadRequest("There is no comments in this announcement");
            }

            _context.Comments.Remove(commentDb);
            return Ok(commentDb);
        }
    }
}
