namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class TwoFactorLoginDto
    {
        public string UserId { get; set; }        // The user's ID
        public string TwoFactorCode { get; set; } // The 6-digit code from the authenticator app
        public bool RememberMe { get; set; }      // If user wants to remain signed in
    }
}