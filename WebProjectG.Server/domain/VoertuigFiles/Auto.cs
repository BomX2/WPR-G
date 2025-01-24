using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectG.Server.domain.VoertuigFiles
{
    public class Auto
    {
        [Key]
        public String Kenteken { get; set; }

        [ForeignKey("Kenteken")]
        public Voertuig Voertuig { get; set; } = null!;

        public int AantalDeuren { get; set; }
        public string BrandstofType { get; set; }
        public bool HeeftAirco { get; set; }
        public double BrandstofVerbruik { get; set; } // in liter per 100 km
        public string TransmissieType { get; set; }
        public int Bagageruimte { get; set; } // in liters

        public Auto() { }
        public Auto(string kenteken, Voertuig voertuig, int aantalDeuren, string brandstofType, bool heeftAirco, double brandstofVerbruik, string transmissieType, int bagageruimte)
        {
            Kenteken = kenteken;
            Voertuig = voertuig;
            AantalDeuren = aantalDeuren;
            BrandstofType = brandstofType;
            HeeftAirco = heeftAirco;
            BrandstofVerbruik = brandstofVerbruik;
            TransmissieType = transmissieType;
            Bagageruimte = bagageruimte;
        }
    }
}
