﻿using System;

namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementSentEvent : IMeasurement
    {
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; } //light intensity
        public decimal Humidity { get; set; }
        public string SensorName { get; set; }

        public MeasurementSentEvent(decimal temperature, int illuminance, decimal humidity, string sensorName)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
            SensorName = sensorName;
        }
    }
}