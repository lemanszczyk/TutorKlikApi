
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorklik.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }

        [Required]
        [MaxLength(300)]
        public string AnnouncementName { get; set; }

        [Required]
        [MaxLength(5000)]
        public string AnnouncementDescription { get; set; }
        public string? Tags { get; set; }
        public List<Comment> Comments { get; set; }
        [Required]
        public User Author { get; set; }
    }
}
