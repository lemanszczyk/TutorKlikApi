using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models.ModelsDto
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password are not the same")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserType { get; set; }

        public string? ProfileImage { get; set; }
    }
}
