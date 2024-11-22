namespace WebProjectG.Server.domain
{
    public abstract class Klant
    {
        public string Naam { get; set; }
        public string Adres { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }
        protected Klant(string Naam,string adres,int telefoonnummer,string email)
        {
            this.Naam = Naam;
            this.Adres = adres;
            this.Telefoonnummer = telefoonnummer;
            this.Email = email;
        }
    }

}
