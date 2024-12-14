using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public DbSet<Gebruiker.Gebruiker> gebruikers { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
        public HuurContext() { }
      public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void OnConfiguring() 
        { 
          
        }
    }
}
