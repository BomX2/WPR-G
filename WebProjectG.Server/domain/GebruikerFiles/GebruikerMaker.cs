﻿/*using WebProjectG.Server.domain.EmailObserver;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class GebruikerMaker : IObservable
    { 
        /*
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer) => observers.Add(observer);
        public void Detach(IObserver observer) => observers.Remove(observer);

        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        public Klant MaakParticulierAccount(string naam,string adres, int telefoonnummer, string email)
        {
            //Maakt nieuwe particulier gebruiker
            var particulier = new Particulier(naam, adres, telefoonnummer, email);
            //log en notify
            Console.WriteLine($"Particulier account aangemaakt: {naam} ({email})");
            Notify($"Particulier account aangemaakt voor {naam} ({email})");
            return particulier;
        }

        public Klant MaakZakelijkeHuurderAccount(string naam, string adres, int telefoonnummer, string email)
        {
            //Maakt nieuwe zakelijke huurder gebruiker
            var zakelijkHuurder = new ZakelijkeHuurder(naam, adres, telefoonnummer, email);
            //log en notify
            Console.WriteLine($"Zakelijk account aangemaakt: {naam} ({email})");
            Notify($"Zakelijk account aangemaakt voor {naam} ({email})");
            return zakelijkHuurder;
        }

        public Klant MaakWagenParkBeheerderAccount(string naam, string adres, int telefoonnummer, string email)
        { 
            //Maakt nieuwe wagenparkbeheerder gebruiker
            var wagenParkBeheerder = new WagenParkBeheerder(naam, adres, telefoonnummer, email);
            //log en notify
            Console.WriteLine($"WagenparkBeheerder account aangemaakt: {naam} ({email})");
            Notify($"WagenparkBeheerder account aangemaakt voor {naam} ({email})");
            return wagenParkBeheerder;
        }
/*
        public Bedrijf MaakBedrijfsAccount(string bedrijfsnaam, string kvknummer, string domeinnaam, string adres) 
        {
            //logic for if wagenparkbeheerder is active.
            if (Klant.getActiveWagenparkBeheerder == null)
            {
                throw new InvalidOperationException("Een actieve WagenParkBeheerder is vereist om een bedrijfsprofiel aan te maken.");
            }
            //Maakt nieuwe bedrijf profiel
            var bedrijf = new Bedrijf(bedrijfsnaam, kvknummer, domeinnaam, adres);
            // log n notify
            Console.WriteLine($"Bedrijfs profiel account aangemaakt: {bedrijfsnaam}");
            Notify($"Bedrijfprofiel aangemaakt voor {wagenparkbeheerder.Naam} ({wagenparkbeheerder.Email})");
            return bedrijf;
        }

    }
}
*/
