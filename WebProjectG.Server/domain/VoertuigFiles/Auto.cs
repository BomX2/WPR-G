namespace WebProjectG.Server.domain.Voertuig
{
    public class Auto : Voertuig
    {
        public int Id { get; set; }
        public int AantalDeuren { get; set; }
        public string BrandstofType { get; set; }
        public bool HeeftAirco { get; set; }
        public double BrandstofVerbruik { get; set; } // in liter per 100 km
        public string TransmissieType { get; set; }
        public int Bagageruimte { get; set; } // in liters

        public Auto(string huurStatus, string merk, string type, string kenteken, string kleur, int aanschafJaar, decimal prijsPerDag, bool inclusiefVerzekering,
            int aantalDeuren, string brandstofType, bool heeftAirco, double brandstofVerbruik, string transmissieType, int bagageruimte)
            : base(huurStatus, merk, type, kenteken, kleur, aanschafJaar, prijsPerDag, inclusiefVerzekering)
        {
            AantalDeuren = aantalDeuren;
            BrandstofType = brandstofType;
            HeeftAirco = heeftAirco;
            BrandstofVerbruik = brandstofVerbruik;
            TransmissieType = transmissieType;
            Bagageruimte = bagageruimte;
        }
    }
}
