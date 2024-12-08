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
        public List<Gebruiker> gebruikers { get; private set; } = [];
        private Abonnement Abonnement { get; set; } = new Abonnement();
         
    }
}
