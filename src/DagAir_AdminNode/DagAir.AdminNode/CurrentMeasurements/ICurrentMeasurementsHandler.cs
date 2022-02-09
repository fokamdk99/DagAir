using System.Threading.Tasks;

namespace DagAir.AdminNode.CurrentMeasurements
{
    public interface ICurrentMeasurementsHandler
    {
        Task Handle(string sensorName);
    }
}