using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagAir.IngestionNode.Contracts
{
    public interface IMeasurementsInsertedEvent
    {
        IMeasurement Measurement { get; }
        string SensorId { get; }
    }
}
