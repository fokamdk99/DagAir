using System.Net.Http;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.SensorState;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.CurrentMeasurements
{
    public class CurrentMeasurementsHandler : ICurrentMeasurementsHandler
    {
        private readonly HttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<CurrentMeasurementsHandler> _logger;

        public CurrentMeasurementsHandler(HttpClient client, 
            IExternalServices externalServices, 
            ILogger<CurrentMeasurementsHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }

        public async Task Handle(string sensorName)
        {
            var path = _externalServices.SensorState + SensorStateEndpoints.GetCurrentMeasurements + sensorName;
            await _client.GetAsync(path);
            _logger.LogInformation($"Requested current measurement from sensor: {sensorName}");
        }
    }
}