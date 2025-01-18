namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class LoginDto
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}