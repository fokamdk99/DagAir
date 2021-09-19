using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DagAir.IngestionNode.Data.UserContext
{
    public interface IDagAirUserContext
    {
        public DatabaseFacade Database { get; }
        public DbSet<Room> Rooms { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}