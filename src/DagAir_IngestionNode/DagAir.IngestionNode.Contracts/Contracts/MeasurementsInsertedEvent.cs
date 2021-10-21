namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementsInsertedEvent : IMeasurementsInsertedEvent
    {
        public IMeasurement Measurement { get; set; }
        public string SensorId { get; set; }

        public MeasurementsInsertedEvent(IMeasurement measurement, string sensorId)
        {
            Measurement = measurement;
            SensorId = sensorId;
        }
    }
}