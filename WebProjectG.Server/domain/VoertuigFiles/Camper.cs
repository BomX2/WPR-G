using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectG.Server.domain.VoertuigFiles
{
    public class Camper
    {
        [Key]
        public String Kenteken { get; set; }

        [ForeignKey("Kenteken")]
        public Voertuig Voertuig { get; set; } = null!;

        public double Lengte { get; set; } //in meters
        public double Hoogte { get; set; } //in meters
        public int Slaapplaatsen { get; set; }
        public bool HeeftBadkamer { get; set; }
        public bool HeeftKeuken {  get; set; }
        public double WaterTankCapaciteit { get; set; } // in liters
        public double AfvalTankCapaciteit { get; set; } // in liters
        public double BrandstofVerbruik { get; set; } // per 100 km
        public bool HeeftZonnepanelen { get; set; }
        public int FietsRekCapaciteit { get; set; }
        public bool HeeftLuifel {  get; set; }

        public Camper() { }
        public Camper(string kenteken, Voertuig voertuig, double lengte, double hoogte, int slaapplaatsen, bool heeftBadkamer, bool heeftKeuken,
                      double waterTankCapaciteit, double afvalTankCapaciteit, double brandstofVerbruik, bool heeftZonnepanelen,
                      int fietsRekCapaciteit, bool heeftLuifel)
        {
            Kenteken = kenteken;
            Voertuig = voertuig;
            Lengte = lengte;
            Hoogte = hoogte;
            Slaapplaatsen = slaapplaatsen;
            HeeftBadkamer = heeftBadkamer;
            HeeftKeuken = heeftKeuken;
            WaterTankCapaciteit = waterTankCapaciteit;
            AfvalTankCapaciteit = afvalTankCapaciteit;
            BrandstofVerbruik = brandstofVerbruik;
            HeeftZonnepanelen = heeftZonnepanelen;
            FietsRekCapaciteit = fietsRekCapaciteit;
            HeeftLuifel = heeftLuifel;
        }
    }
}
