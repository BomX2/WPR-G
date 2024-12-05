namespace WebProjectG.Server.domain.Gebruiker
{
    public class Klant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }
        public Klant()
        {

        }
        public Klant(string Naam, string adres, int telefoonnummer, string email)
        {
            this.Naam = Naam;
            this.Adres = adres;
            this.Telefoonnummer = telefoonnummer;
            this.Email = email;
        }
       
    }
}