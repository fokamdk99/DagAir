using System.Reflection;
using DagAir.IngestionNode.Data.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.IngestionNode.Data.UserContext
{
    public class DagAirUserContext : DbContext, IDagAirUserContext
    {
        private const string Schema = "dagair_usr";

        public DagAirUserContext()
        {
            
        }

        public DagAirUserContext(DbContextOptions<DagAirUserContext> options) : base(options)
        {
            
        }
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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}