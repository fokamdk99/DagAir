namespace DagAir.IngestionNode.Contracts
{
    public interface ISensorLocation : ILocation
    {
        int Floor { get; }
        string RoomNumber { get; } //string to cover cases like room number 12a, 12b etc.
    }
}