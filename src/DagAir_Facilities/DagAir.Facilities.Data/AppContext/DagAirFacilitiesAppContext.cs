using System.Reflection;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Data.AppContext
{
    public class DagAirFacilitiesAppContext : DbContext, IDagAirFacilitiesAppContext
    {
        private const string Schema = "DagAir.Facilities";
        
        public DagAirFacilitiesAppContext() {}

        public DagAirFacilitiesAppContext(DbContextOptions<DagAirFacilitiesAppContext> options) : base(options) {}
        
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Affiliate> Affiliates { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

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
            modelBuilder.SeedAffiliates();
            modelBuilder.SeedOrganizations();
            modelBuilder.SeedRooms();
        }
    }
}