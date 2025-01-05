    using System.ComponentModel.DataAnnotations.Schema;
    using WebProjectG.Server.domain.Voertuig;

    namespace WebProjectG.Server.domain.Huur
    {
    public class Aanvraag
    {
        public int Id { get; set; }
        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public string PersoonsGegevens { get; set; }
        public string Email { get; set; }
        public int Telefoonnummer { get; set; }
        public bool? Goedgekeurd { get; set; } = false;

        public Aanvraag(DateOnly startDatum, DateOnly eindDatum, string persoonsGegevens, string email, int telefoonnummer, bool? goedgekeurd )
            {
                StartDatum = startDatum;
                EindDatum = eindDatum;
                PersoonsGegevens = persoonsGegevens;
                Email = email;
                Telefoonnummer = telefoonnummer;
                Goedgekeurd = goedgekeurd;
            }
        }
    }
