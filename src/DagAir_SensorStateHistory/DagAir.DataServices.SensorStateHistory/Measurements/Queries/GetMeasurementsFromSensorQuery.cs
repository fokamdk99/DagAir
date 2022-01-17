using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Components.Influx;
using DagAir.DataServices.SensorStateHistory.Contracts.Commands;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using InfluxDB.Client;

namespace DagAir.DataServices.SensorStateHistory.Measurements.Queries
{
    public class GetMeasurementsFromSensorQuery : IGetMeasurementsFromSensorQuery
    {
        private readonly InfluxDBClient _client;
        private readonly IInfluxConfiguration _influxConfiguration;
        private readonly IInfluxHelper _influxHelper;

        private const string MeasurementType = "influxroommeasurement";

        public GetMeasurementsFromSensorQuery(InfluxDBClient client, 
            IInfluxConfiguration influxConfiguration, 
            IInfluxHelper influxHelper)
        {
            _client = client;
            _influxConfiguration = influxConfiguration;
            _influxHelper = influxHelper;
        }

        public async Task<List<HistoricMeasurement>> GetMeasurementsFromSensor(
            GetMeasurementsFromSensorCommand getMeasurementsFromSensorCommand)
        {
            var temperatureQuery = CreateInfluxQuery(
                "temperature", getMeasurementsFromSensorCommand.SensorName, getMeasurementsFromSensorCommand.NumberOfRecords);
            var illuminanceQuery = CreateInfluxQuery(
                "illuminance", getMeasurementsFromSensorCommand.SensorName, getMeasurementsFromSensorCommand.NumberOfRecords);
            var humidityQuery = CreateInfluxQuery(
                "humidity", getMeasurementsFromSensorCommand.SensorName, getMeasurementsFromSensorCommand.NumberOfRecords);
            var organizationId = await _influxHelper.GetOrganizationIdByOrganizationName(_client, _influxConfiguration);
            var temperatureResults = await _client.GetQueryApi().QueryAsync(temperatureQuery, organizationId);
            var illuminanceResults = await _client.GetQueryApi().QueryAsync(illuminanceQuery, organizationId);
            var humidityResults = await _client.GetQueryApi().QueryAsync(humidityQuery, organizationId);

            if (temperatureResults.Count == 0)
            {
                return new List<HistoricMeasurement>();
            }
            
            var temperatureRecords = temperatureResults.ElementAt(0).Records;
            var illuminanceRecords = illuminanceResults.ElementAt(0).Records;
            var humidityRecords = humidityResults.ElementAt(0).Records;

            int numberOfRecords = getMeasurementsFromSensorCommand.NumberOfRecords;
            var 
            retrievedNumberOfRecords = temperatureResults.ElementAt(0).Records.Count;
            if (numberOfRecords > retrievedNumberOfRecords)
            {
                numberOfRecords = retrievedNumberOfRecords;
            }
            
            
            
            List<HistoricMeasurement> historicMeasurements = new List<HistoricMeasurement>();
            for (int i = retrievedNumberOfRecords-1; i >= retrievedNumberOfRecords-numberOfRecords; i--)
            {
                var historicMeasurement = new HistoricMeasurement(
                    Convert.ToDecimal(temperatureRecords.ElementAt(i).GetValue()),
                    Convert.ToInt32(illuminanceRecords.ElementAt(i).GetValue()),
                    Convert.ToDecimal(humidityRecords.ElementAt(i).GetValue()),
                    temperatureRecords.ElementAt(i).GetTimeInDateTime().GetValueOrDefault());
                
                historicMeasurements.Add(historicMeasurement);
            }
            
            return historicMeasurements;
        }
        
        private string CreateInfluxQuery(string field, string sensorName, int numberOfRecords)
        {
            return $"from(bucket: \"{_influxConfiguration.BucketName}\")\n" +
                   $"|> range(start: 0)\n" + 
                   $"|> filter(fn: (r) => (r._measurement == \"{MeasurementType}\" " +
                   $"and r._field == \"{field}\" " +
                   $"and r.sensor_name == \"{sensorName}\"))\n" +
                   //$"|> limit(n: {numberOfRecords})" +
                   $"|> sort(columns: [\"_time\"])";
        }
    }
}