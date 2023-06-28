using System.ComponentModel.DataAnnotations;

namespace Tutorklik.Models.ModelsDto
{
    public class UserPasswordDto
    {
        public int UserId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
