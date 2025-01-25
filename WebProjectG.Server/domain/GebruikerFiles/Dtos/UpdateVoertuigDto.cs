using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.GebruikerFiles.Dtos
{
    public class UpdateVoertuigDto
    {
        [Required]
        public string Kenteken { get; set; }

        [Required]
        public string HuurStatus { get; set; }

        [Required]
        public string Merk { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Kleur { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "AanschafJaar must be between 1900 and 2100.")]
        public int AanschafJaar { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "PrijsPerDag must be a positive value.")]
        public decimal PrijsPerDag { get; set; }

        [Required]
        public bool InclusiefVerzekering { get; set; }

        [Required]
        [RegularExpression("^(auto|camper|caravan)$", ErrorMessage = "Soort must be 'auto', 'camper', or 'caravan'.")]
        public string Soort { get; set; }
    }
}
