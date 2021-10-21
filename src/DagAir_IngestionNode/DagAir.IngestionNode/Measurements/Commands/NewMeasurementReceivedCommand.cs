using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.Measurements.Commands
{
    public class NewMeasurementReceivedCommand
    {
        public IMeasurement Measurement { get; set; }
        public string SensorId { get; set; }

        public NewMeasurementReceivedCommand(IMeasurement measurement, string sensorId)
        {
            Measurement = measurement;
            SensorId = sensorId;
        }
    }
}