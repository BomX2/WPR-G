namespace WebProjectG.Server.domain.Medewerker
{
    abstract public class Medewerker
    {
        public int MedewerkerID { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        protected Medewerker(int medewerkerID, string name, int accessLevel)
        {
            MedewerkerID = medewerkerID;
            Name = name;
            AccessLevel = accessLevel;
        }

        abstract public void VoertuigStatusBeheren();

    }
}
