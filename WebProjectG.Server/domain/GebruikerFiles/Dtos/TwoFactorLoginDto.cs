namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class TwoFactorLoginDto
    {
        public string UserId { get; set; }        
        public string TwoFactorCode { get; set; } 
        public bool RememberMe { get; set; }     
    }
}