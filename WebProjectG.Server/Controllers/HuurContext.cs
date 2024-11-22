using Microsoft.EntityFrameworkCore;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        public DbSet<Klant> klanten;
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
      public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void OnConfiguring() 
        { 
          
        }
    }
}
