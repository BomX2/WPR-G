namespace WebProjectG.Server.domain
{
    public class Bedrijf : Klant
    {
      public string BedrijfsNaam { get; set; }
      public int Kvknummer { get; set; }
       public string Domeinnaam { get; set; }
        public List<ZakelijkeHuurder> ZakelijkeHuurders { get; private set; }
        public List<WagenParkBeheerder> WagenParkBeheerders { get; private set; }

        public Bedrijf(string adres, string telefoonnummer, string email,
                       string bedrijfsNaam, int kvknummer, string domeinnaam)
            : base(adres, telefoonnummer, email) 
        { 
            BedrijfsNaam = bedrijfsNaam;
            Kvknummer = kvknummer;
            Domeinnaam = domeinnaam;
            ZakelijkeHuurders = new List<ZakelijkeHuurder>();
            WagenParkBeheerders = new List<WagenParkBeheerder>();
        }
        }

    }
}
