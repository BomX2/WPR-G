using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Huur;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebProjectG.Server.domain.BedrijfFiles
{
    public class Bedrijf
    {
        public Bedrijf() { }

        public Bedrijf(string bedrijfsNaam, string adres,string kvknummer, string domeinNaam, Abonnement? abonnement )
        {
            BedrijfsNaam = bedrijfsNaam;
            Adres = adres;
            KvkNummer = kvknummer;
            DomeinNaam = domeinNaam;
            Abonnement = abonnement;

        }

        public string BedrijfsNaam { get; set; }
        
        [Key] public string KvkNummer { get; init; }
        public string DomeinNaam { get; set; }
        public string Adres { get; set; }

        public List<Gebruiker>? ZakelijkeHuurders { get; private set; }
        public Abonnement? Abonnement { get; set; }

    }
}