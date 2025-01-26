namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class CamperDto : UpdateVoertuigDto
    {
        public string Kenteken { get; set; }
        public double Lengte { get; set; } // in meters
        public double Hoogte { get; set; } // in meters
        public int Slaapplaatsen { get; set; }
        public bool HeeftBadkamer { get; set; }
        public bool HeeftKeuken { get; set; }
        public double WaterTankCapaciteit { get; set; } // in liters
        public double AfvalTankCapaciteit { get; set; } // in liters
        public double BrandstofVerbruik { get; set; } // per 100 km
        public bool HeeftZonnepanelen { get; set; }
        public int FietsRekCapaciteit { get; set; }
        public bool HeeftLuifel { get; set; }
    }
}
