using System.ComponentModel.DataAnnotations;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class WagenParkBeheerder : Klant {
        [Key] int Id { get; set; }
        private WagenParkBeheerder() { }
       public WagenParkBeheerder(int id, string naam, string adres, int telefoonnummer, string email) : base(naam, adres, telefoonnummer, email) 
        {
              Id = id;
        }
    }
}
