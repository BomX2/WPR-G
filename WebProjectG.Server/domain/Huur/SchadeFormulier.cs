using System.Security.Cryptography.Xml;
using WebProjectG.Server.domain.VoertuigFiles;

namespace WebProjectG.Server.domain.Huur
{
    public class SchadeFormulier
    {
        public int Id { get; set; }
          public string SchadeType { get; set; }
          public string? ErnstVDSchade { get; set; }
           public string Email { get; set; }
           public string Telefoonnummer { get; set; }
           public string Kenteken { get; set; }
          public Voertuig? Voertuig { get; set; }
         public int AanvraagId { get; set; }
         public Aanvraag? aanvraag { get; set; }
        public SchadeFormulier(string schadeType, string email, string telefoonnummer, string kenteken, int aanvraagId, string? ernstVDSchade)
        {
            Email = email;
            Telefoonnummer = telefoonnummer;
            SchadeType = schadeType;
            Kenteken = kenteken;
            AanvraagId = aanvraagId;
            ErnstVDSchade = ernstVDSchade;
        }
    }
}
