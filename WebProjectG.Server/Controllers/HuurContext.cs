using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
        public HuurContext() { }
        public DbSet<Gebruiker.Gebruiker> gebruikers { get; set; }
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Bedrijf> Bedrijven { get; set; }
      
    }
}
