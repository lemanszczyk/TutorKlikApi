
using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models
{
    public class Annoucement
    {
        public int AnnoucementId { get; set; }

        [Required]
        [MaxLength(300)]
        public string AnnoucementName { get; set; }

        [Required]
        [MaxLength(5000)]
        public string AnnoucementDescription { get; set; }
        public string Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }

        [Required]
        public User Author { get; set; }
    }
}
