namespace DagAir.Facilities.Rooms.Models
{
    public record CurrentRoomReadModel(
        long Id,
        string Number,
        int Floor,
        long AffiliateId,
        long OrganizationId);
}