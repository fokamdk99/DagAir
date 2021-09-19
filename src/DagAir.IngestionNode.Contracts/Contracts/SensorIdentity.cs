namespace DagAir.IngestionNode.Contracts
{
    public class SensorIdentity : ISensorIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public SensorIdentity(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}