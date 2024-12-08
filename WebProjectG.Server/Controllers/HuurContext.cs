using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public DbSet<Gebruiker.Gebruiker> klanten { get; set; }
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
        public HuurContext() { }
      public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Klant> klanten { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
