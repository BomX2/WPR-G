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
            public int AutoId {  get; set; }
            [ForeignKey("AutoId")]
            public Auto Auto { get; set; }

            public Aanvraag(DateOnly startDatum, DateOnly eindDatum , string persoonsGegevens, string email, int telefoonnummer, int autoId)
            {
                StartDatum = startDatum;
                EindDatum = eindDatum;
                PersoonsGegevens = persoonsGegevens;
                Email = email;
                Telefoonnummer = telefoonnummer;
                AutoId = autoId;
            }
        }
    }
