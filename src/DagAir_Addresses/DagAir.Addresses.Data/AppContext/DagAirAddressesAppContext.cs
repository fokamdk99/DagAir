using System.Reflection;
using DagAir.Addresses.Data.AppEntities;
using DagAir.Addresses.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Data.AppContext
{
    public class DagAirAddressesAppContext : DbContext, IDagAirAddressesAppContext
    {
        private const string Schema = "DagAir.Addresses";
        
        public DagAirAddressesAppContext() {}
        
        public DagAirAddressesAppContext(DbContextOptions<DagAirAddressesAppContext> options) : base(options) {}
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema(Schema);
            
            SeedData(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedAddresses();
            modelBuilder.SeedCities();
            modelBuilder.SeedCountries();
            modelBuilder.SeedPostalCodes();
        }
    }
}