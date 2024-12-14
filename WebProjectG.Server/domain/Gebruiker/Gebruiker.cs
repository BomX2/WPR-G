using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Gebruiker : IdentityUser
    {
        public string Adres { get; set; }

        public Bedrijf? Bedrijf { get; set; }

    }
}