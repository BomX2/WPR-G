namespace WebProjectG.Server.domain
{
    public abstract class Klant
    {
            public string Adres {  get; set; }
            public int Telefoonnummer { get; set; }
            public string Email { get; set; }
            
        public Klant (string adres, int telefoonnummer, string email)
        {
          Adres = adres;
          Telefoonnummer = telefoonnummer;
          Email = email;
        }
    }

}
