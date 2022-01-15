using DagAir.AdminNode.Contracts.DTOs;

namespace WebAdminApp1.Rooms.Models
{
    public class RoomModel
    {
        public string Environment { get; set; }
        public AdminNodeRoomDto AdminNodeRoomDto { get; set; }
        public AdminNodeAffiliateDto AdminNodeAffiliateDto { get; set; }
    }
}