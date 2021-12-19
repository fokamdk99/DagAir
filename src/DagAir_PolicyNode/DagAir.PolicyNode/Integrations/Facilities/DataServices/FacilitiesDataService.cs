using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.PolicyNode.Integrations.Facilities.DataServices
{
    public class FacilitiesDataService : IFacilitiesDataService
    {
        private readonly DagAirHttpClient _client;
        private readonly IServicesUrls _servicesUrls;

        public FacilitiesDataService(DagAirHttpClient client, IServicesUrls servicesUrls)
        {
            _client = client;
            _servicesUrls = servicesUrls;
        }

        public async Task<RoomDto> GetRoomByRoomId(long roomId)
        {
            var url = _servicesUrls.FacilitiesApi + ApiRoutes.GetRoomByRoomId + roomId;
            var (room, statusCode) = await _client.GetAsync<RoomDto>(url);
            if (statusCode == HttpStatusCode.OK)
            {
                return room;
            }
            
            throw new Exception($"No room with id {roomId} has been found");
        }
    }
}