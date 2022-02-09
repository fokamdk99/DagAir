using System.Threading.Tasks;

namespace DagAir.DataServices.SensorState.CurrentMeasurements.Handlers
{
    public interface IGetCurrentMeasurementHandler
    {
        Task Handle(string sensorName);
    }
}