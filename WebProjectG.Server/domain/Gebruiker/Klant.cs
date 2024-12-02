namespace WebProjectG.Server.domain.Gebruiker
{
    public abstract class Klant
    {
        public string Naam { get; set; }
        public string Adres { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }
        protected Klant(string Naam, string adres, int telefoonnummer, string email)
        {
            this.Naam = Naam;
            Adres = adres;
            Telefoonnummer = telefoonnummer;
            Email = email;
        }

        protected Klant(string naam, string adres, string telefoonnummer, string email) {
            Naam = naam;
            Adres = adres;
            Email = email;
        }
    }

}
