using System.ComponentModel.DataAnnotations.Schema;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Voertuig;

namespace WebProjectG.Server.domain.Huur
{
    public class Aanvraag
    {
        public int Id { get; set; }
        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public string? Status { get; set; }
        public bool? Goedgekeurd { get; set; } = false;

        public String Gebruikerid { get; set; }
        [ForeignKey("Gebruikerid")]
        public Gebruiker? Gebruiker { get; set; }

        public int AutoId { get; set; }
        [ForeignKey("AutoId")]
        public Auto? Auto { get; set; }

        public Aanvraag(DateOnly startDatum, DateOnly eindDatum,String gebruikerid, bool? goedgekeurd, int autoId)
        {
            StartDatum = startDatum;
            EindDatum = eindDatum;
            Gebruikerid = gebruikerid;
            Goedgekeurd = goedgekeurd;
            AutoId = autoId;
        }
    }
}
