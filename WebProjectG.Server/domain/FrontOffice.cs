namespace WebProjectG.Server.domain
{
    public class FrontOffice : Medewerker
    {
        public FrontOffice(int medewerkerID, string name, int accessLevel) : base(medewerkerID, name, accessLevel)
        {
        }

        public override void VoertuigStatusBeheren()
        {
            throw new NotImplementedException();
        }
    }
}
