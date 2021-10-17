using System.Reflection;
using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Data.AppContext
{
    public class DagAirSensorAppContext : DbContext, IDagAirSensorAppContext
    {
        private const string Schema = "DagAir";
        
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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}