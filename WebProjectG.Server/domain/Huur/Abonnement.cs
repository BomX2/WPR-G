
﻿using System.ComponentModel.DataAnnotations.Schema;
using WebProjectG.Server.domain.BedrijfFiles;


namespace WebProjectG.Server.domain.Huur
{
    public class Abonnement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AbonnementID { get; set; }
        public string AbonnementType { get; set; }
        public decimal Prijs { get; set; }
        public DateTime StartTime { get; set; } // Start datum abonnement
        public DateTime? EndTime { get; set; } // Eind datum, nullable voor maandelijks/onbepaald abbo

        public List<Bedrijf> Bedrijven { get; set; } = new();

        public TimeSpan? Duur
        {
            get
            {
                if (EndTime == null) return null; // Ongoing subscription, no duration left to calculate
                if (EndTime < DateTime.Now) return TimeSpan.Zero; // Subscription has expired
                return EndTime - DateTime.Now; // Time remaining
            }
        }

        public bool IsActive => DateTime.Now >= StartTime && (EndTime == null || DateTime.Now <= EndTime);

        public Abonnement()
        {

        }
        public Abonnement(int abonnementID, string abonnementType, decimal prijs, DateTime startTime, DateTime? endTime)
        {
            AbonnementID = abonnementID;
            AbonnementType = abonnementType;
            Prijs = prijs;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}