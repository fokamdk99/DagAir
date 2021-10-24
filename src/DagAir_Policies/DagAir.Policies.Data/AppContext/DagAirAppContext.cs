using System.Reflection;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Data.AppContext
{
    public class DagAirAppContext : DbContext, IDagAirAppContext
    {
        private const string Schema = "DagAir.Policies";
        
        public DagAirAppContext() {}
        
        public DagAirAppContext(DbContextOptions<DagAirAppContext> options) : base(options) {}
        
        public DbSet<RoomPolicy> RoomPolicies { get; set; }
        public DbSet<RoomPolicyCategory> RoomPolicyConfigurations { get; set; }
        public DbSet<ExpectedRoomConditions> ExpectedRoomConditions { get; set; }

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