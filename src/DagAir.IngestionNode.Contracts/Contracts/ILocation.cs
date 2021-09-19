namespace DagAir.IngestionNode.Contracts
{
    public interface ILocation
    {
        string Id { get; }
        string Country { get; }
        string City { get; }
    }
}