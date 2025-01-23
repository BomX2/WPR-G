using Microsoft.Build.Framework;

namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class VerifyTokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
