using System;
using DagAir.IngestionNode.Contracts;
using NodaTime;

namespace DagAir.DataServices.SensorStateHistory.Contracts.Contracts
{
    public class HistoricMeasurement : IMeasurement
    {
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Date { get; set; }
        
        public HistoricMeasurement(decimal temperature, 
            int illuminance, 
            decimal humidity,
            DateTime date)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
            Date = date;
        }
    }
}