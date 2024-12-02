using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public DbSet<Klant> klanten;

      public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void OnConfiguring() 
        { 
          
        }
    }
}
