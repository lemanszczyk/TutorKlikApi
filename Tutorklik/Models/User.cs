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


        public string Email { get; set; }
        public Role UserType {get; set;} 

    }
    public enum Role
    {
        Tutor,
        Student
    }
}
