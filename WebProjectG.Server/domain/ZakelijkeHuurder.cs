using WebProjectG.Server.domain.Gebruiker;

namespace WebProjectG.Server.domain
{
    public class ZakelijkeHuurder : Klant
    {
        public ZakelijkeHuurder(string Naam, string adres, int telefoonnummer, string email) : base(Naam, adres,telefoonnummer, email)
        {
        }
    }
}
