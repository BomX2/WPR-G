using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Bedrijf(int id,string bedrijfsNaam, string kvknummer, string adres) 
    {
        public int Id { get; set; } = id;
        public string BedrijfsNaam { get; set; } = bedrijfsNaam;
        public string Adres { get; set; } = adres;

        public string Kvknummer { get; init; } = kvknummer;
        public List<ZakelijkeHuurder> ZakelijkeHuurders { get; private set; } = new List<ZakelijkeHuurder>();
        public List<WagenParkBeheerder> WagenParkBeheerders { get; private set; } = new List<WagenParkBeheerder>();
        private Abonnement Abonnement { get; set; } = new Abonnement();
         
    }
}
