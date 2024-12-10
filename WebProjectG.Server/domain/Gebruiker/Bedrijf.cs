using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
namespace WebProjectG.Server.domain.Gebruiker
{
    public class Bedrijf(string bedrijfsNaam, string kvknummer, string domeinnaam, string adres) 
    {
        public string BedrijfsNaam { get; set; } = bedrijfsNaam;
        [Key] public string Kvknummer { get; init; } = kvknummer;
        public string Domeinnaam { get; set; } = domeinnaam;
        public string Adres { get; set; } = adres;
     
        public List<Gebruiker> ZakelijkeHuurders { get; private set; } = new List<Gebruiker>();
        private Abonnement Abonnement { get; set; } = new Abonnement();

    }
}
