using System.ComponentModel.DataAnnotations.Schema;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.VoertuigFiles;

namespace WebProjectG.Server.domain.Huur
{
    public class Aanvraag
    {
        public int Id { get; set; }
        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public string Ophaaltijd {  get; set; }
        public string Inlevertijd { get; set; }
        public string? Status { get; set; }
        public string  Email { get; set; }
        public string Adres { get; set; }
        public string Telefoonnummer { get; set; }
        public bool? Goedgekeurd { get; set; } = false;
        public string? Gebruikerid { get; set; }
        [ForeignKey("Gebruikerid")]
        public Gebruiker? Gebruiker { get; set; }

        public String Kenteken { get; set; }
        [ForeignKey("Kenteken")]
        public Voertuig? voertuig { get; set; }

        public Aanvraag()
        {
        }
        public  Aanvraag(DateOnly startDatum,String ophaalTijd, string email, string adres, string telefoonnummer,DateOnly eindDatum,String inleverTijd,string? gebruikerid, bool? goedgekeurd, String kenteken)
        {
            StartDatum = startDatum;
            Ophaaltijd = ophaalTijd;
            EindDatum = eindDatum;
            Inlevertijd = inleverTijd;
            Email = email;
            Adres = adres;
            Telefoonnummer = telefoonnummer;
            Gebruikerid = gebruikerid;
            Goedgekeurd = goedgekeurd;
            Kenteken = kenteken;
        }
    }
}
