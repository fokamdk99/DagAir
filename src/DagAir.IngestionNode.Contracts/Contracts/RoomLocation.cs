namespace DagAir.IngestionNode.Contracts
{
    public class RoomLocation : IRoomLocation
    {
        public int Floor { get; }
        public string RoomNumber { get; } //string to cover cases like room number 12a, 12b etc.
        public string Id { get; }
        public string Country { get; }
        public string City { get; }
    }
}