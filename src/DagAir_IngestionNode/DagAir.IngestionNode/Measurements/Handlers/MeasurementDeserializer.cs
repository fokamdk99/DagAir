using System;
using System.Linq;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class MeasurementDeserializer : IMeasurementDeserializer
    {
        private const int NumberOfParameters = 4;
        private readonly ILogger<MeasurementDeserializer> _logger;

        public MeasurementDeserializer(ILogger<MeasurementDeserializer> logger)
        {
            _logger = logger;
        }

        public NewMeasurementReceivedCommand DeserializeMeasurement(string message)
        {
            try
            {
                string[] parameters = message.Split(";");
                if (parameters.Length != NumberOfParameters)
                {
                    throw new Exception(
                        $"Measurement sent by a sensor is invalid. Excepted number of parameters: {NumberOfParameters}, given: {parameters.Length}");
                }
                
                decimal temperature = (decimal)Math.Round(decimal.Parse(parameters.ElementAt(0)), 2);
                int illuminance = int.Parse(parameters.ElementAt(1));
                decimal humidity = (decimal)Math.Round(decimal.Parse(parameters.ElementAt(2)), 2);

                var measurement = new RoomMeasurement(
                    temperature, 
                    illuminance, 
                    humidity);
                return new NewMeasurementReceivedCommand(measurement, parameters.ElementAt(3));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception();
            }
        }
    }
}