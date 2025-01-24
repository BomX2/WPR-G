using WebProjectG.Server.domain.Huur;

namespace WebProjectG.Server.domain.BedrijfFiles
{
    public class BedrijfPutDto
    {

        public string AbonnementType { get; set; }
        public decimal Prijs { get; set; }
        public string BetaalMethode { get; set; }
        public string Periode { get; set; }

    }
}
