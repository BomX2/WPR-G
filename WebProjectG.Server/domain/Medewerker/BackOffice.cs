﻿namespace WebProjectG.Server.domain.Medewerker
{
    public class BackOffice : Medewerker
    {
        public BackOffice(int medewerkerID, string name, int accessLevel) : base(medewerkerID, name, accessLevel)
        {
        }

        public override void VoertuigStatusBeheren()
        {
            throw new NotImplementedException();
        }
    }
}
