namespace WebProjectG.Server.domain
{
    abstract public class Voertuig
    {
        public string HuurStatus { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Kenteken { get; set; }
        public string Kleur { get; set; }
        public int AanschafJaar { get; set; }

        protected Voertuig(string huurStatus, string merk, string type, string kenteken, string kleur, int aanschafJaar)
        {
            HuurStatus = huurStatus;
            Merk = merk;
            Type = type;
            Kenteken = kenteken;
            Kleur = kleur;
            AanschafJaar = aanschafJaar;
        }

    }
}
