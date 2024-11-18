namespace WebProjectG.Server.domain
{
    public abstract class Bedrijf
    {

      public string BedrijfsNaam { get; set; }
      public int Kvknummer { get; set; }
       public string Domeinnaam { get; set; }
        public List<ZakelijkeHuurder> zakelijkeHuurders = [];
        public Bedrijf(string bedrijfsNaam, int kvknummer, string domeinnaam) 
        { 
            BedrijfsNaam = bedrijfsNaam;
            Kvknummer = kvknummer;
            Domeinnaam = domeinnaam;
        }

    }
}
