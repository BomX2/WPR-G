using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Gebruiker;
namespace WebProjectG.Server.domain
{
    public class HuurContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server(localdb)\\MSSQLLocalDB;Database=CarAndAll_database;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Klant> klanten { get; set; }
        public DbSet<Particulier> Particuliers { get; set; }
        public DbSet<ZakelijkeHuurder> ZakelijkeHuurders { get; set; }
        public DbSet<WagenParkBeheerder> wagenParkBeheerders { get; set; }
        public HuurContext(DbContextOptions<HuurContext> options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
