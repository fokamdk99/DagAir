using System;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.PolicyNode.Integrations.Facilities.DataServices;

namespace DagAir.PolicyNode.Tests
{
    public class TestFacilitiesDataService : IFacilitiesDataService
    {
        public Task<RoomDto> GetRoomByRoomId(long roomId)
        {
            return Task.Run(() => CreateNewRoomDto(roomId));

        }

        private RoomDto CreateNewRoomDto(long roomId)
        {
            return new RoomDto
            {
                Id = roomId,
                Number = "133",
                Floor = 1,
                AffiliateId = 1
            };
        }
    }
}