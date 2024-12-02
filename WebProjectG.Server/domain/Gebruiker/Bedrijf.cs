using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Bedrijf(string bedrijfsNaam, int kvknummer, string domeinnaam, string adres) 
    {
        public string BedrijfsNaam { get; set; } = bedrijfsNaam;
        public int Kvknummer { get; init; } = kvknummer;
        public string Domeinnaam { get; set; } = domeinnaam;
        public string Adres { get; set; } = adres;
        public List<ZakelijkeHuurder> ZakelijkeHuurders { get; private set; } = new List<ZakelijkeHuurder>();
        public List<WagenParkBeheerder> WagenParkBeheerders { get; private set; } = new List<WagenParkBeheerder>();
        private Abonnement Abonnement { get; set; } = new Abonnement();

    }
}
