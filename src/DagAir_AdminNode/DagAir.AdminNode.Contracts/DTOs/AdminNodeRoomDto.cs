using DagAir.Facilities.Contracts.DTOs;
using DagAir.Policies.Contracts.DTOs;

namespace DagAir.AdminNode.Contracts.DTOs
{
    public class AdminNodeRoomDto
    {
        public RoomDto RoomDto { get; set; }
        public PastMeasurementsDto PastMeasurements { get; set; }
    }
}