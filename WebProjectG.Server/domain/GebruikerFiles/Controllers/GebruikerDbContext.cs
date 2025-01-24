using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.BedrijfFiles;
using WebProjectG.Server.domain.Huur;
namespace WebProjectG.Server.domain.GebruikerFiles.Controllers
{
    public class GebruikerDbContext : IdentityDbContext<Gebruiker>
    {
        public DbSet<Bedrijf> Bedrijven { get; set; } 
        public DbSet<Abonnement> Abonnementen { get; set; }

        public GebruikerDbContext(DbContextOptions<GebruikerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Gebruiker>(entity =>
            {
                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(256); 

                entity.Property(e => e.PhoneNumber)
                    .IsRequired();
            });
        }
    }
}