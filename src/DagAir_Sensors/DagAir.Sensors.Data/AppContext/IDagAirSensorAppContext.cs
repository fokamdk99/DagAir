using System.Threading;
using System.Threading.Tasks;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DagAir.Sensors.Data.AppContext
{
    public interface IDagAirSensorAppContext
    {
        public DatabaseFacade Database { get; }
        public IModel Model { get; }
        public DbSet<Sensor> Sensors { get; }
        public DbSet<SensorModel> SensorModels { get; }
        public DbSet<Producer> Producers { get; }
        

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}