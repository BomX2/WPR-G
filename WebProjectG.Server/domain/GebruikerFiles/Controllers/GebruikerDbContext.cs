using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Gebruiker;

namespace WebProjectG.Server.domain.GebruikerFiles.Controllers
{
    public class GebruikerDbContext : IdentityDbContext<Gebruiker.Gebruiker>
    {
        public DbSet<Bedrijf> Bedrijven { get; set; } // Include Bedrijf table in the schema

        public GebruikerDbContext(DbContextOptions<GebruikerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Gebruiker.Gebruiker>(entity =>
            {
                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(256); // Adjust length as necessary

                entity.Property(e => e.PhoneNumber)
                    .IsRequired();
            });
        }
    }
}