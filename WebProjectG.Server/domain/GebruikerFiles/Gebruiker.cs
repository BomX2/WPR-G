using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WebProjectG.Server.domain.BedrijfFiles;

namespace WebProjectG.Server.domain.GebruikerFiles
{
    public class Gebruiker : IdentityUser
    {
        public string Adres { get; set; }

        public Bedrijf? Bedrijf { get; set; }
        
        [ForeignKey(nameof(Bedrijf))]
        public string? KvkNummer { get; set; }

        public string? Role { get; set; }
    }
}