using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.GebruikerFiles;
using WebProjectG.Server.domain.Huur;

namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
       public DbSet<Aanvraag> Aanvragen { get; set; }
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
