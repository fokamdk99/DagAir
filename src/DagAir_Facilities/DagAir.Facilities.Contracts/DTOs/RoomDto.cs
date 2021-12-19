using System;

namespace DagAir.Facilities.Contracts.DTOs
{
    public class RoomDto
    {
        public long Id { get; set; }
        public Guid UniqueRoomId { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public long AffiliateId { get; set; }
        public AffiliateDto Affiliate { get; set; }
    }
}