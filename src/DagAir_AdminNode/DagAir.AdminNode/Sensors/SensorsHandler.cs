using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Sensors;
using DagAir.Components.HttpClients;
using DagAir.Sensors.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Sensors
{
    public class SensorsHandler : ISensorsHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<SensorsHandler> _logger;

        public SensorsHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<SensorsHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }
        
        public async Task<SensorDto> GetSensorByRoomId(long roomId)
        {
            var path = _externalServices.SensorsDataService + SensorsDataServiceEndpoints.GetSensorByRoomId + roomId;
            var response = await _client.GetAsync<SensorDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                /*
                var message =
                    $"Error while trying to get information about sensors. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
                */
                return null;
            }

            return response.Item1;
        }
    }
}