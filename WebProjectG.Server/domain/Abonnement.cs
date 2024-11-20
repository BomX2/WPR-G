namespace WebProjectG.Server.domain
{
    public class Abonnement
    {
        public int AbonnementID { get; set; } 
        public string AbonnementType{ get; set; } 
        public decimal Prijs { get; set; } 
        public DateTime StartTime { get; set; } // Start datum abonnement
        public DateTime? EndTime { get; set; } // Eind datum, nullable voor maandelijks/onbepaald abbo
        public TimeSpan? Duur
        {
            get
            {
                if (EndTime == null) return null; // Ongoing subscription, no duration left to calculate
                if (EndTime < DateTime.Now) return TimeSpan.Zero; // Subscription has expired
                return EndTime - DateTime.Now; // Time remaining
            }
        }

        // Calculated property to determine if the subscription is active
        public bool IsActive => DateTime.Now >= StartTime && (EndTime == null || DateTime.Now <= EndTime);

    }
}