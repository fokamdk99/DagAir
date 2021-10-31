using System.Reflection;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Data.AppContext
{
    public class DagAirPoliciesAppContext : DbContext, IDagAirPoliciesAppContext
    {
        private const string Schema = "DagAir.Policies";
        
        public DagAirPoliciesAppContext() {}
        
        public DagAirPoliciesAppContext(DbContextOptions<DagAirPoliciesAppContext> options) : base(options) {}
        
        public DbSet<RoomPolicy> RoomPolicies { get; set; }
        public DbSet<RoomPolicyCategory> RoomPolicyCategories { get; set; }
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
            
            SeedData(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRoomPolicyConfigurations();
            modelBuilder.SeedExpectedRoomConditions();
            modelBuilder.SeedRoomPolicies();
        }
    }
}