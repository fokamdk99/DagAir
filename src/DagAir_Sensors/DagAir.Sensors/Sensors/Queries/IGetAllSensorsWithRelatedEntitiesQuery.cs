using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Sensors.Data.AppEntities;

namespace DagAir.Sensors.Sensors.Queries
{
    public interface IGetAllSensorsWithRelatedEntitiesQuery
    {
        Task<List<Sensor>> Execute();
    }
}