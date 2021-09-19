using System.Collections.Generic;

namespace DagAir.IngestionNode.Contracts
{
    public class Organization : IOrganization
    {
        public string Id { get; }
        public string Name { get; }
        public ICollection<IDivision> Divisions { get; }
    }
}