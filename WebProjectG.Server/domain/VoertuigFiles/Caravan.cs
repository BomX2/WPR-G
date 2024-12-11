namespace WebProjectG.Server.domain.Voertuig
{
    public class Caravan : Voertuig
    {
        public double Lengte { get; set; } // in meters
        public int Slaapplaatsen { get; set; }
        public bool HeeftKeuken { get; set; }
        public double WaterTankCapaciteit { get; set; } // in liters
        public double AfvalTankCapaciteit { get; set; } // in liters
        public bool HeeftLuifel { get; set; }

        public Caravan(string huurStatus, string merk, string type, string kenteken, string kleur, int aanschafJaar, decimal prijsPerDag, bool inclusiefVerzekering,
            double lengte, int slaapplaatsen, bool heeftKeuken, double waterTankCapaciteit, double afvalTankCapaciteit, bool heeftLuifel)
            : base(huurStatus, merk, type, kenteken, kleur, aanschafJaar, prijsPerDag, inclusiefVerzekering)
        {
            Lengte = lengte;
            Slaapplaatsen = slaapplaatsen;
            HeeftKeuken = heeftKeuken;
            WaterTankCapaciteit = waterTankCapaciteit;
            AfvalTankCapaciteit = afvalTankCapaciteit;
            HeeftLuifel = heeftLuifel;
        }
    }
}
