using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebProjectG.Server.domain.Gebruiker
{
    public class GebruikerDbContext : IdentityDbContext<Gebruiker>
    {
        public GebruikerDbContext(DbContextOptions<GebruikerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

