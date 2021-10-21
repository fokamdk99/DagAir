using System.Reflection;
using DagAir.QueryNode.Data.AppEntitities;
using DagAir.Sensors.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Data.AppContext
{
    public class DagAirSensorAppContext : DbContext, IDagAirSensorAppContext
    {
        private const string Schema = "DagAir.Sensors";
        
        public DagAirSensorAppContext() {}

        public DagAirSensorAppContext(DbContextOptions<DagAirSensorAppContext> options) : base(options) {}
        
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<SensorModel> SensorModels { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }

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
            modelBuilder.SeedProducers();
            modelBuilder.SeedSensorModels();
            modelBuilder.SeedSensors();
        }
    }
}