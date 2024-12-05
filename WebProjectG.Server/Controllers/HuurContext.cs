using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarAndAll_database;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Klant> klanten { get; set; }
     
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
