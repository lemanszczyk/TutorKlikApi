using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models.ModelsDto
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        [MaxLength(3000)]
        public string? Description { get; set; }

        [Range(0, 5)]
        public decimal Rate { get; set; }
        public UserDto? Author { get; set; }

        [Required] public  int AnnouncementId { get; set; }

        public static implicit operator CommentDto(Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.CommentId,
                Description = comment.Description,
                Rate = comment.Rate,
                Author = comment.Author,
                AnnouncementId = comment.AnnouncementId,
            };
        }
    }
}
