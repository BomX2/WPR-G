using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Huur;
namespace WebProjectG.Server.domain.BedrijfFiles
{
    public class Bedrijf
    {
        public Bedrijf() { }

        public Bedrijf(string bedrijfsNaam, string kvknummer, string domeinnaam, string adres)
        {
            BedrijfsNaam = bedrijfsNaam;
            KvkNummer = kvknummer;
            Domeinnaam = domeinnaam;
            Adres = adres;
            ZakelijkeHuurders = new List<Gebruiker>();
        }

        public string BedrijfsNaam { get; set; }
        [Key] public string KvkNummer { get; init; }
        public string Domeinnaam { get; set; }
        public string Adres { get; set; }

        public List<Gebruiker> ZakelijkeHuurders { get; private set; }
        private Abonnement? Abonnement { get; set; } = new Abonnement();

    }
}
