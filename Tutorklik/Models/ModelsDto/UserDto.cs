using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Tutorklik.Models.ModelsDto
{
    public class UserDto
    {
        public int UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string UserType { get; set; }
        public string? ProfileImage { get; set; }

        public static implicit operator UserDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                UserType = user.UserType,
                ProfileImage = user.ProfileImage,
            };
        }
    }
}
