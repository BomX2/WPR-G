using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;
using WebProjectG.Server.domain.Voertuig;

namespace WebProjectG.Server.domain.Huur
{
    public class AanvraagDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public bool? Goedgekeurd { get; set; } = false;
      
    }
}
