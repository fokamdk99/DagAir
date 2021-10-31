using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Policies.Contracts.DTOs;

namespace DagAir.PolicyNode.Integrations.Policies.DataServices
{
    public class PoliciesDataService : IPoliciesDataService
    {
        private readonly IDagAirHttpClient _client;
        private readonly IServicesUrls _servicesUrls;

        public PoliciesDataService(IDagAirHttpClient client, IServicesUrls servicesUrls)
        {
            _client = client;
            _servicesUrls = servicesUrls;
        }

        public async Task<RoomPolicyDto> GetRoomPolicyByRoomId(long roomId)
        {
            var url = _servicesUrls.PoliciesApi + ApiRoutes.GetRoomPolicyByRoomId + roomId;
            var (sensor, statusCode) = await _client.GetAsync<RoomPolicyDto>(url);
            if (statusCode == HttpStatusCode.OK)
            {
                return sensor;
            }
            else
            {
                throw new Exception($"No valid room policy with room id {roomId} has been found");
            }
        }
    }
}