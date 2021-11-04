using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.IngestionNode.Integrations.Sensors.DataServices
{
    public class SensorsDataService : ISensorsDataService
    {
        private readonly DagAirHttpClient _client;
        private readonly IServicesUrls _servicesUrls;

        public SensorsDataService(DagAirHttpClient client, IServicesUrls servicesUrls)
        {
            _client = client;
            _servicesUrls = servicesUrls;
        }

        public async Task<SensorDto> GetSensorById(string id)
        {
            var url = _servicesUrls.SensorsApi + ApiRoutes.GetSensorBySensorId + id;
            var (sensor, statusCode) = await _client.GetAsync<SensorDto>(url);
            if (statusCode == HttpStatusCode.OK)
            {
                return sensor;
            }
            else
            {
                throw new Exception($"Sensor with id {id} has not been found");
            }
        }
    }
}