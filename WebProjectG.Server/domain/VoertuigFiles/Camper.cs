namespace WebProjectG.Server.domain.Voertuig
{
    public class Camper : Voertuig
    {
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

        public Camper(string huurStatus, string merk, string type, string kenteken, string kleur, int aanschafJaar, decimal prijsPerDag, bool inclusiefVerzekering,
            double lengte, double hoogte, int slaapplaatsen, bool heeftBadkamer,bool heeftKeuken,double waterTankCapaciteit, double afvalTankCapaciteit, 
            double brandstofVerbruik, bool heeftZonnepanelen, int fietsRekCapaciteit, bool heeftLuifel)
            : base(huurStatus, merk, type, kenteken, kleur, aanschafJaar, prijsPerDag, inclusiefVerzekering)
        {
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
