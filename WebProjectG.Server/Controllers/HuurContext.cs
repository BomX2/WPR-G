using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.datetime_converter;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Huur;
using WebProjectG.Server.domain.Voertuig;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
       public DbSet<Aanvraag> Aanvragen { get; set; }
        public DbSet<Auto> autos { get; set;}
              public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
        public HuurContext() { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aanvraag>()
            .Property(a => a.StartDatum)
            .HasConversion<TijdConverter>();
            modelBuilder.Entity<Aanvraag>()
                .Property(a => a.EindDatum)
                .HasConversion<TijdConverter>();
        }
        public void OnConfiguring() 
        { 
          
        }
    }
}
