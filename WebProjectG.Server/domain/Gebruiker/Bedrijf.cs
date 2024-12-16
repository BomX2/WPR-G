using System.   ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Bedrijf 
    {
        
        public int Id { get; set; }
        public string BedrijfsNaam { get; set; }
        public string Adres { get; set; } 
        public string Kvknummer { get; init; }
        public string DomeinNaam { get; set; }
        public Abonnement? Abonnement { get; set; }
        public List<Gebruiker> gebruikers { get; private set; } = [];
        public Bedrijf ()
        {

        }
        public Bedrijf(int id, string bedrijfsNaam, string adres, string kvknummer, string domeinnaam, Abonnement? abonnement = null)
        {
            Id = id;
            BedrijfsNaam = bedrijfsNaam;
            Adres = adres;
            Kvknummer = kvknummer;
            DomeinNaam = domeinnaam;
            Abonnement = abonnement;
        }
    }
}
