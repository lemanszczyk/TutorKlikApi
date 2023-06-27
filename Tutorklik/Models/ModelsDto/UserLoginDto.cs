using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models.ModelsDto
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
