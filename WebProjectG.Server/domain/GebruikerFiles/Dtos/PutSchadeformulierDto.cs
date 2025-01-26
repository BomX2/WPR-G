using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class PutSchadeformulierDto
    {
        public string SchadeType { get; set; }
        public string ErnstVDSchade { get; set; }
        public string Kenteken { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public int AanvraagId { get; set; }
    }
}
