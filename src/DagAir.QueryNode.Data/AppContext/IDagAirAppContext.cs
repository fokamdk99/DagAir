using System.Threading;
using System.Threading.Tasks;
using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DagAir.QueryNode.Data.AppContext
{
    public interface IDagAirAppContext
    {
        public DatabaseFacade Database { get; }
        public IModel Model { get; }

        public DbSet<Organization> Organizations { get; }
        public DbSet<Affiliate> Affiliates { get; }
        public DbSet<Sensor> Sensors { get; }
        public DbSet<Room> Rooms { get; }
        public DbSet<SensorModel> SensorModels { get; }
        public DbSet<Producer> Producers { get; }
        public DbSet<Country> Countries { get; }
        public DbSet<Address> Addresses { get; }
        public DbSet<PostalCode> PostalCodes { get; }
        public DbSet<City> Cities { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}