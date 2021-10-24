using System.Threading;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DagAir.Policies.Data.AppContext
{
    public interface IDagAirAppContext
    {
        public DatabaseFacade Database { get; }
        public IModel Model { get; }
        
        public DbSet<RoomPolicy> RoomPolicies { get; set; }
        public DbSet<RoomPolicyCategory> RoomPolicyConfigurations { get; set; }
        public DbSet<ExpectedRoomConditions> ExpectedRoomConditions { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}