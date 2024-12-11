using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.Gebruiker.Dtos
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
