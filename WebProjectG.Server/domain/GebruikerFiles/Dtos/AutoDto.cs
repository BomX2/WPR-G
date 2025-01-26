namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class AutoDto : UpdateVoertuigDto
    {
        public string Kenteken { get; set; }
        public int AantalDeuren { get; set; }
        public string BrandstofType { get; set; }
        public bool HeeftAirco { get; set; }
        public double BrandstofVerbruik { get; set; } // in liter per 100 km
        public string TransmissieType { get; set; }
        public int Bagageruimte { get; set; } // in liters
    }
}
