using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Gebruiker : IdentityUser
    {
        public string? Adres {  get; set; }
        public string? BedrijfsDomainNaam { get; set; }
    }
}