using System.Reflection;
using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.QueryNode.Data.AppContext
{
    public class DagAirAppContext : DbContext, IDagAirAppContext
    {
        private const string Schema = "DagAir";
        
        public DagAirAppContext() {}

        public DagAirAppContext(DbContextOptions<DagAirAppContext> options) : base(options) {}
        
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Affiliate> Affiliates { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<SensorModel> SensorModels { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<PostalCode> PostalCodes { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema(Schema);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}