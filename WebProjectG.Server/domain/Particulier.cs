namespace WebProjectG.Server.domain
{
    public class Particulier : Klant
    {
        public Abonnement Abonnement { get; set; }

        public Particulier(string Naam, string adres, int telefoonnummer, string email) 
            : base(Naam, adres, telefoonnummer, email)
        {
        }
    }
}
