namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementsInsertedEvent : IMeasurementsInsertedEvent
    {
        public IMeasurement Measurement { get; set; }
        public ISensorIdentity SensorIdentity { get; set; }

        public MeasurementsInsertedEvent(IMeasurement measurement, ISensorIdentity sensorIdentity)
        {
            Measurement = measurement;
            SensorIdentity = sensorIdentity;
        }
    }
}