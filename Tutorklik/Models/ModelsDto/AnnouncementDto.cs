using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models.ModelsDto
{
    public class AnnouncementDto
    {
        public int AnnoucementId { get; set; }

        [Required]
        [MaxLength(300)]
        public string AnnoucementName { get; set; }

        [Required]
        [MaxLength(5000)]
        public string AnnoucementDescription { get; set; }
        public string? Tags { get; set; }
        public List<Tuple<int, decimal>> CommentsIdAndRate { get; set; }
        [Required]
        public UserDto Author { get; set; }

        public static implicit operator AnnouncementDto(Announcement announcement)
        {
            return new AnnouncementDto
            {
                AnnoucementId = announcement.AnnouncementId,
                AnnoucementName = announcement.AnnouncementName,
                AnnoucementDescription = announcement.AnnouncementDescription,
                Tags = announcement.Tags,
                CommentsIdAndRate = announcement.Comments.Select(x => new Tuple<int, decimal>(x.CommentId, x.Rate)).ToList(),
                Author = announcement.Author,
            };
        }
    }
}
