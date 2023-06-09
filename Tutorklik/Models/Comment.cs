

using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [MaxLength(3000)]
        public string? Description { get; set; }

        [Range(0, 5)]
        public decimal Rate { get; set; }
        public User Author { get; set; }
    }
}
