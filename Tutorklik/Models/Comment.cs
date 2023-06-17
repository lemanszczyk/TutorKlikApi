

using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [MaxLength(3000)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal Rate { get; set; }
        
        [Required]
        public User Author { get; set; }

        [Required]
        public int AnnouncementId { get; set; }
    }
}
