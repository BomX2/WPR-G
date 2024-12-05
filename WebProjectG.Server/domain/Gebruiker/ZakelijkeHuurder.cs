using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class ZakelijkeHuurder : Klant
    {
        [Key] int Id  { get; set; }
        private ZakelijkeHuurder() { }
        public ZakelijkeHuurder(int id,string Naam, string adres, int telefoonnummer, string email) : base(Naam, adres, telefoonnummer, email)
        {
           Id = id;
        }
    }
}
