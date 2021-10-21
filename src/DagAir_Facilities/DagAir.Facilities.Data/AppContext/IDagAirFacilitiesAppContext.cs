using System.Threading;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DagAir.Facilities.Data.AppContext
{
    public interface IDagAirFacilitiesAppContext
    {
        public DatabaseFacade Database { get; }
        public IModel Model { get; }
        public DbSet<Organization> Organizations { get; }
        public DbSet<Affiliate> Affiliates { get; }
        public DbSet<Room> Rooms { get; }
        

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}