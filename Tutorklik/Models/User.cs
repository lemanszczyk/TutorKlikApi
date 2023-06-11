using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorklik.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string UserType {get; set;}

        // There are two types of User Tutor and Student
    }
}
