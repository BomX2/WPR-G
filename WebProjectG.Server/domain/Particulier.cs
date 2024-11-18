namespace WebProjectG.Server.domain
{
    public class Particulier : Klant
    {
        public string Name { get; set; }
        
        public Particulier (string name, string adres, int telefoonnummer, string email)
            :base(adres, telefoonnummer, email)
        {
            Name = name;
        }
    }
}
