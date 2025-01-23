namespace WebProjectG.Server.domain.Voertuig
{
    public class Voertuig
    {
        public string HuurStatus { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Kenteken { get; set; }
        public string Kleur { get; set; }
        public int AanschafJaar { get; set; }
        public decimal PrijsPerDag { get; set; }
        public bool InclusiefVerzekering { get; set; }
        

        protected Voertuig(string huurStatus, string merk, string type, string kenteken, string kleur, int aanschafJaar, decimal prijsPerDag, bool inclusiefVerzekering)
        {
            HuurStatus = huurStatus;
            Merk = merk;
            Type = type;
            Kenteken = kenteken;
            Kleur = kleur;
            AanschafJaar = aanschafJaar;
            PrijsPerDag = prijsPerDag;
            InclusiefVerzekering = inclusiefVerzekering;
        }
    }
}
