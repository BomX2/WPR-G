using System.Security.Cryptography.Xml;
using WebProjectG.Server.domain.VoertuigFiles;

namespace WebProjectG.Server.domain.Huur
{
    public class SchadeFormulier
    {
        public int Id { get; set; }
          public string SchadeType { get; set; }
          public string Kenteken { get; set; }
          public Voertuig? Voertuig { get; set; }
         public int AanvraagId { get; set; }
         public Aanvraag? aanvraag { get; set; }
        public SchadeFormulier(string schadeType, string kenteken, int aanvraagId)
        {
            SchadeType = schadeType;
            Kenteken = kenteken;
            AanvraagId = aanvraagId;
        }
    }
}
