using System.Threading.Tasks;
using DagAir.Sensors.Data.AppEntities;

namespace DagAir.Sensors.Sensors.Queries
{
    public interface IGetSensorWithRelatedEntitiesQuery
    {
        Task<Sensor> Execute(long id);
    }
}