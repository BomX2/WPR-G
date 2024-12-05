using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class Particulier : Klant
    {
        
       [Key] int Id { get; set; }
        private Particulier() { }
        public Particulier(int id,string naam, string adres, int telefoonnummer, string email) : base(naam, adres, telefoonnummer, email)
        {
            Id = id;
        }
    }
}
