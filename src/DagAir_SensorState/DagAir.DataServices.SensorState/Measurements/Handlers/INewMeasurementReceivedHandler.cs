using System.Threading.Tasks;
using DagAir.DataServices.SensorState.Contracts.Commands;

namespace DagAir.DataServices.SensorState.Measurements.Handlers
{
    public interface INewMeasurementReceivedHandler
    {
        Task Handle(NewMeasurementReceivedCommand command);
    }
}