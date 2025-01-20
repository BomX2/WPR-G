using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectG.Server.domain.VoertuigFiles
{
    public class Caravan
    {
        [Key]
        public string Kenteken { get; set; }

        [ForeignKey("Kenteken")]
        public Voertuig Voertuig { get; set; } = null!;

        public double Lengte { get; set; } // in meters
        public int Slaapplaatsen { get; set; }
        public bool HeeftKeuken { get; set; }
        public double WaterTankCapaciteit { get; set; } // in liters
        public double AfvalTankCapaciteit { get; set; } // in liters
        public bool HeeftLuifel { get; set; }

        public Caravan() { }
        public Caravan(string kenteken, Voertuig voertuig, double lengte, int slaapplaatsen, bool heeftKeuken, double waterTankCapaciteit,
                       double afvalTankCapaciteit, bool heeftLuifel)
        {
            Kenteken = kenteken;
            Voertuig = voertuig;
            Lengte = lengte;
            Slaapplaatsen = slaapplaatsen;
            HeeftKeuken = heeftKeuken;
            WaterTankCapaciteit = waterTankCapaciteit;
            AfvalTankCapaciteit = afvalTankCapaciteit;
            HeeftLuifel = heeftLuifel;
        }
    }
}
