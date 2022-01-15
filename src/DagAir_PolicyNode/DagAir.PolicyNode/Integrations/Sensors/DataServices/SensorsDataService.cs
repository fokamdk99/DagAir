using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Sensors.Contracts.Commands;
using DagAir.Sensors.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.PolicyNode.Integrations.Sensors.DataServices
{
    public class SensorsDataService : ISensorsDataService
    {
        private readonly DagAirHttpClient _client;
        private readonly IServicesUrls _servicesUrls;
        private readonly ILogger<SensorsDataService> _logger;

        public SensorsDataService(DagAirHttpClient client, 
            IServicesUrls servicesUrls, 
            ILogger<SensorsDataService> logger)
        {
            _client = client;
            _servicesUrls = servicesUrls;
            _logger = logger;
        }

        public async Task<SensorDto> GetSensorBySensorName(string sensorName)
        {
            var url = _servicesUrls.SensorsApi + SensorsDataServiceEndpoints.GetSensorBySensorName;

            GetSensorBySensorNameCommand getSensorBySensorNameCommand = new GetSensorBySensorNameCommand
            {
                SensorName = sensorName
            };
            
            var (sensorDto, statusCode) = await _client
                .PostAsync<GetSensorBySensorNameCommand, SensorDto>(url, getSensorBySensorNameCommand);
            if (statusCode != HttpStatusCode.OK)
            {
                string message = $"Error while trying to retrieve sensor by sensor name {sensorName}";
                _logger.LogError(message);
                throw new Exception(message);
            }
            
            return sensorDto;
        }
    }
}