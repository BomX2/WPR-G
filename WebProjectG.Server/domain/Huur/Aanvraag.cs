namespace WebProjectG.Server.domain.Huur
{
    public class Aanvraag
    {
        public int Id { get; set; }
        public string StartDatum { get; set; }
        public string PersoonsGegevens { get; set; }
        public string Email { get; set; }
        public int Telefoonnummer { get; set; }

        public Aanvraag(string startDatum, string persoonsGegevens, string email, int telefoonnummer)
        {
            StartDatum = startDatum;
            PersoonsGegevens = persoonsGegevens;
            Email = email;
            Telefoonnummer = telefoonnummer;
        }
    }
}
