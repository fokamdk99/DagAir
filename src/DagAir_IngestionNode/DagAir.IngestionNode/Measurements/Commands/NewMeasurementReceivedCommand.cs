using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.Measurements.Commands
{
    public class NewMeasurementReceivedCommand
    {
        public IMeasurement Measurement { get; set; }
        public string SensorName { get; set; }

        public NewMeasurementReceivedCommand(IMeasurement measurement, string sensorName)
        {
            Measurement = measurement;
            SensorName = sensorName;
        }
    }
}