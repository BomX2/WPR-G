using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class GebruikerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Role {  get; set; }

    }
}