namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class CaravanDto : UpdateVoertuigDto
    {
        public string Kenteken { get; set; }
        public double Lengte { get; set; } // in meters
        public int Slaapplaatsen { get; set; }
        public bool HeeftKeuken { get; set; }
        public double WaterTankCapaciteit { get; set; } // in liters
        public double AfvalTankCapaciteit { get; set; } // in liters
        public bool HeeftLuifel { get; set; }
    }
}
