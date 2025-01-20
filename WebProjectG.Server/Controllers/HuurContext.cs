using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebProjectG.Server.datetime_converter;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Huur;
using WebProjectG.Server.domain.VoertuigFiles;

namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
       public DbSet<Aanvraag> Aanvragen { get; set; }
        public DbSet<Voertuig> Voertuigen{ get; set; }
        public DbSet<Auto> autos { get; set;}
        public DbSet<Camper> campers { get; set; }
        public DbSet<Caravan> caravans { get; set; }
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

            modelBuilder.Entity<Auto>()
                    .HasOne(a => a.Voertuig)
                    .WithOne()
                    .HasForeignKey<Auto>(a => a.Kenteken);

            modelBuilder.Entity<Camper>()
                .HasOne(c => c.Voertuig)
                .WithOne()
                .HasForeignKey<Camper>(c => c.Kenteken);

            modelBuilder.Entity<Caravan>()
                .HasOne(c => c.Voertuig)
                .WithOne()
                .HasForeignKey<Caravan>(c => c.Kenteken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarAndAll_database;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
