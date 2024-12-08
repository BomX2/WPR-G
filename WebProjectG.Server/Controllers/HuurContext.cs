using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
        public HuurContext() { }
      public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Gebruiker.Gebruiker> gebruikers { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
      
    }
}
