namespace WebProjectG.Server.domain
{
    abstract public class Medewerker
    {
        public int MedewerkerID { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        protected Medewerker (int medewerkerID, string name, int accessLevel)
        {
            this.MedewerkerID = medewerkerID;
            this.Name = name;
            this.AccessLevel = accessLevel;
        }

        abstract public void VoertuigStatusBeheren();

    }
}
